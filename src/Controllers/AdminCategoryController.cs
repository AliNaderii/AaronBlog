using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aaron.Models.Entities;
using Aaron.Data;
using Aaron.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Aaron.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
    [Route("admin/category/[action]")]
    public class AdminCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page)
        {
            var categoriesQuery =  _context.Categories.AsQueryable();
            var paginatedCategories = await PaginatedList<Category>.CreateAsync(categoriesQuery, page, 10);
            var categoryViewModel = new CategoryViewModel()
            {
                Categories = paginatedCategories,
            };
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var category = model.NewCategory;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == model.NewCategory.Id);

            if (existingCategory is null)
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(model.NewCategory.Name))
            {
                return RedirectToAction("Index");
            }

            existingCategory.Name = model.NewCategory.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            var category = await _context.Categories.FindAsync(model.NewCategory.Id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


    }
}
