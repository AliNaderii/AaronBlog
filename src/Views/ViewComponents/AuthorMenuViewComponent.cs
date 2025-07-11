using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aaron.Data;

namespace Aaron.Views.ViewComponents
{
    public class AuthorMenuViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AuthorMenuViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var authors = await _context.Authors
                .OrderBy(a => a)
                .AsNoTracking()
                .ToListAsync();

            return View(authors);
        }
    }
}
