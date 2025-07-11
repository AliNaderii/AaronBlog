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
    [Route("admin/authors/[action]")]
    public class AdminAuthorsController : Controller
    {
        private readonly AppDbContext _context;
        public AdminAuthorsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    // Check for uploaded file format
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var validExtensions = new[] { ".jpeg", ".jpg", ".png" };

                    if (!model.ImageFile.ContentType.StartsWith("image/") || !validExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ImageFile", "فقط فایل های تصویری مجاز هستند");
                        return View(model);
                    }

                    // Check for uploaded file size
                    const long maxFileSize = 1 * 1024 * 1024; // 1MB

                    if (model.ImageFile.Length > maxFileSize)
                    {
                        ModelState.AddModelError("ImageFile", "حجم فایل نباید بیشتر از ۱ مگابایت باشد.");
                        return View(model);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/authors", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    var author = new Author()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        CoverImagePath = "/images/authors/" + fileName
                    };

                    await _context.AddAsync(author);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return NotFound();

            var authorViewModel = new EditAuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                CoverImagePath = author.CoverImagePath,
            };

            return View(authorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAuthorViewModel model, List<int> selectedTags)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == model.Id);

            if (existingAuthor is null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            existingAuthor.Name = model.Name;
            existingAuthor.Description = model.Description;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                var validExtensions = new[] { ".jpeg", ".jpg", ".png" };
                if (!model.ImageFile.ContentType.StartsWith("image/") || !validExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "فقط فایل های تصویری مجاز هستند");
                    model.CoverImagePath = existingAuthor.CoverImagePath;
                    return View(model);
                }

                const long maxImageSize = 1 * 1024 * 1024;

                if (model.ImageFile.Length > maxImageSize)
                {
                    ModelState.AddModelError("ImageFile", "حجم فایل نباید بیشتر از ۱ مگابایت باشد.");
                    model.CoverImagePath = existingAuthor.CoverImagePath;
                    return View(model);
                }

                if (!string.IsNullOrWhiteSpace(existingAuthor.CoverImagePath))
                {
                    var oldFilePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", existingAuthor.CoverImagePath.TrimStart('/')
                        );

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/authors", newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                existingAuthor.CoverImagePath = "/images/authors/" + newFileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminAuthors");
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return NotFound();

            return View(author);
        }

        public async Task<IActionResult> Delete (int id)
        {
            var author = await _context.Authors
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (author is null)
            {
                return NotFound();
            }

            _context.Remove(author);
            await _context.SaveChangesAsync();
            return View(nameof(Index));
        }
    }
}
