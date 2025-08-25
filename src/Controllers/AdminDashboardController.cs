using Aaron.Data;
using Aaron.Models.Entities;
using Aaron.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aaron.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
    [Route("admin/dashboard/[action]")]
    public class AdminDashboardController : Controller
    {
        private readonly AppDbContext _context;
        public AdminDashboardController(AppDbContext context)
        {
            _context = context;
        }

        private async Task LoadViewBagsAsync()
        {
            ViewBag.Authors = new SelectList(await _context.Authors.ToListAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name");
        }

        public async Task<IActionResult> Index(string? searchTerm, int? selectedCategory, int? selectedTag, int page)
        {
            var bookQuery = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Tags)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                bookQuery = bookQuery.Where(b => b.Title.Contains(searchTerm) || b.Author.Name.Contains(searchTerm));
            }

            if (selectedCategory is not null)
            {
                bookQuery = bookQuery.Where(b => b.CategoryId == selectedCategory);
            }

            if (selectedTag is not null)
            {
                bookQuery = bookQuery.Where(b => b.Tags.Any(t => t.Id == selectedTag));
            }

            var paginatedBooks = await PaginatedList<Book>.CreateAsync(bookQuery, page, 10);

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", selectedCategory);
            ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name", selectedTag);

            return View(paginatedBooks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadViewBagsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadViewBagsAsync();
                return View(model);
            }

            var tags = await _context.Tags.Where(t => model.SelectedTags.Contains(t.Id)).ToListAsync();

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Check for uploaded file format
                var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                var validExtensions = new[] { ".jpeg", ".jpg", ".png" };

                if (!model.ImageFile.ContentType.StartsWith("image/") || !validExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "فقط فایل های تصویری مجاز هستند");
                    await LoadViewBagsAsync();
                    return View(model);
                }

                // Check for uploaded file size
                const long maxFileSize = 1 * 1024 * 1024; // 1MB

                if (model.ImageFile.Length > maxFileSize)
                {
                    ModelState.AddModelError("ImageFile", "حجم فایل نباید بیشتر از ۱ مگابایت باشد.");
                    await LoadViewBagsAsync();
                    return View(model);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"خطا در ذخیره فایل یا ذخیره کتاب: {ex.Message}");
                        ModelState.AddModelError("ImageFile", "در هنگام ذخیره اطلاعات مشکلی پیش آمد.");
                        await LoadViewBagsAsync();
                        return View(model);
                    }
                }

                var book = new Book()
                {
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    Slug = new Book().GenerateSlug(model.Title),
                    Summary = model.Summary,
                    Description = model.Description,
                    CoverImagePath = "/images/books/" + fileName,
                    CategoryId = model.CategoryId,
                    Tags = tags
                };

                await _context.AddAsync(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Tags)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Slug == slug);

            if (book is null)
                return NotFound();

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.
                Include(b => b.Author).
                Include(b => b.Tags).
                Include(b => b.Category).
                FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return NotFound();

            var bookViewModel = new EditBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                AuthorId = book.AuthorId,
                Summary = book.Summary,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                Category = book.Category.Name,
                CategoryId = book.CategoryId,
            };

            await LoadViewBagsAsync();

            return View(bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookViewModel model)
        {
            var existingBook = await _context.Books
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == model.Id);

            if (existingBook is null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadViewBagsAsync();
                return View(model);
            }

            existingBook.Title = model.Title;
            existingBook.AuthorId = model.AuthorId;
            existingBook.Slug = existingBook.GenerateSlug(model.Title);
            existingBook.Summary = model.Summary;
            existingBook.Description = model.Description;
            existingBook.CategoryId = model.CategoryId;

            existingBook.Tags.Clear();
            var tags = await _context.Tags.Where(t => model.SelectedTags.Contains(t.Id)).ToListAsync();
            foreach (var t in tags)
            {
                existingBook.Tags.Add(t);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                var validExtensions = new[] { ".jpeg", ".jpg", ".png" };
                if (!model.ImageFile.ContentType.StartsWith("image/") || !validExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "فقط فایل های تصویری مجاز هستند");
                    await LoadViewBagsAsync();
                    model.CoverImagePath = existingBook.CoverImagePath;
                    return View(model);
                }

                const long maxImageSize = 1 * 1024 * 1024;

                if (model.ImageFile.Length > maxImageSize)
                {
                    ModelState.AddModelError("ImageFile", "حجم فایل نباید بیشتر از ۱ مگابایت باشد.");
                    await LoadViewBagsAsync();
                    model.CoverImagePath = existingBook.CoverImagePath;
                    return View(model);
                }

                if (!string.IsNullOrWhiteSpace(existingBook.CoverImagePath))
                {
                    var oldFilePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", existingBook.CoverImagePath.TrimStart('/')
                        );

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    try
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"خطا در ذخیره فایل یا ذخیره کتاب: {ex.Message}");
                        ModelState.AddModelError("ImageFile", "در هنگام ذخیره اطلاعات مشکلی پیش آمد.");
                        await LoadViewBagsAsync();
                        return View(model);
                    }
                }

                existingBook.CoverImagePath = "/images/books/" + newFileName;
            }

                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminDashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.
                FindAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                book.CoverImagePath.TrimStart('/'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            
            _context.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
