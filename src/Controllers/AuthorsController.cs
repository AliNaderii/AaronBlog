using Aaron.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aaron.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string author)
        {
            if (author is null)
            {
                return NotFound();
            }

            var existingAuthor = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.Name == author)
                .FirstOrDefaultAsync();

            if (existingAuthor is null)
            {
                return NotFound();
            }

            return View(existingAuthor);
        }
    }
}
