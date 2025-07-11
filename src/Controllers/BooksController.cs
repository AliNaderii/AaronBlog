using Aaron.Data;
using Aaron.Models.Entities;
using Aaron.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aaron.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page, string? searchTerm, int? selectedCategory, int? selectedTag, string? author)
        {
            var tags = await _context.Tags.ToListAsync();
            ViewBag.Tags = new SelectList(tags, "Id", "Name", selectedTag);
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", selectedCategory);
            ViewBag.SearchTerm = searchTerm;

            var booksQuery = _context.Books.
                Include(b => b.Author).
                Include(b => b.Tags).
                Include(b => b.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchTerm) || b.Author.Name.Contains(searchTerm));
            }

            if (selectedCategory is not null)
            {
                booksQuery = booksQuery.Where(b => b.CategoryId == selectedCategory);
            }

            if (selectedTag is not null)
            {
                booksQuery = booksQuery.Where(b => b.Tags.Any(t => t.Id == selectedTag));
            }

            if (author is not null)
            {
                booksQuery = booksQuery.Where(b => b.Author.Name == author);
            }

            booksQuery = booksQuery
                .AsNoTrackingWithIdentityResolution()
                .OrderByDescending(b => b.CreatedAt);

            if (page < 1)
                page = 1;

            var paginatedBooksViewModel = await PaginatedList<Book>.CreateAsync(booksQuery, page, 9);

            return View(paginatedBooksViewModel);
        }

        [Route("books/details/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return NotFound();

            var book = await _context.Books.
                Include(b => b.Tags).
                Include(b => b.Category).
                Include(b => b.Author).
                FirstOrDefaultAsync(b => b.Slug == slug);

            if (book is null)
                return NotFound();

            return View(book);
        }
    }
}
