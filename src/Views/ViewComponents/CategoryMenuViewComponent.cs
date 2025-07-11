using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aaron.Data;

namespace Aaron.Views.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public CategoryMenuViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }
    }
}
