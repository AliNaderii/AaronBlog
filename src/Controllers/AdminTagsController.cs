using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aaron.Models.Entities;
using Aaron.Data;
using Aaron.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Aaron.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
    [Route("admin/tags/[action]")]
    public class AdminTagsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminTagsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page)
        {
            var tagsQuery = _context.Tags.AsQueryable();
            var paginatedTags = await PaginatedList<Tag>.CreateAsync(tagsQuery, page, 10);
            var tagViewModel = new TagViewModel()
            {
                Tags = paginatedTags
            };
            return View(tagViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var tag = model.NewTag;
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == model.NewTag.Id);

            if (existingTag is null)
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(model.NewTag.Name))
            {
                return RedirectToAction("Index");
            }

            existingTag.Name = model.NewTag.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TagViewModel model)
        {
            var tag = await _context.Tags.FindAsync(model.NewTag.Id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
