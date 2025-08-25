using System.Security.Claims;
using Aaron.Data;
using Aaron.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aaron.Controllers
{
    [Route("admin/account/[action]")]
    public class AdminAccountController : Controller
    {
        private readonly AppDbContext _context;
        public AdminAccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminViewModel model)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == model.Username);
            if (admin is null || !BCrypt.Net.BCrypt.Verify(model.Password, admin.PasswordHash))
            {
                ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, admin.UserName),
                new Claim(ClaimTypes.Role, admin.Role),
                new Claim("IsDefault", admin.IsDefaultCredentials.ToString())
            };

            var identity = new ClaimsIdentity(claims, "AdminAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AdminAuth", principal);

            if (admin.IsDefaultCredentials)
            {
                return RedirectToAction("ChangePassword");
            }

            return RedirectToAction("Index", "AdminDashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("AdminAuth");
            return RedirectToAction("login", "AdminAccount");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
        public IActionResult ChangeUsername()
        {
            var model = new ChangeUsernameViewModel() { CurrentUsername = User.Identity?.Name };
            return View(model);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.UserName.ToLower() == model.CurrentUsername.ToLower());

            if (admin == null || !BCrypt.Net.BCrypt.Verify(model.Password, admin.PasswordHash))
            {
                ModelState.AddModelError(nameof(model.Password), "رمز عبور اشتباه است.");
                return View(model);
            }

            admin.UserName = model.NewUsername;
            await _context.SaveChangesAsync();

            // logout و login با claims جدید
            await HttpContext.SignOutAsync("AdminAuth");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "AdminAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("AdminAuth", principal);

            return RedirectToAction("Index", "AdminDashboard");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "AdminAuth", Roles = "Admin")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.UserName.ToLower() == User.Identity.Name.ToLower());
            if (admin == null || !BCrypt.Net.BCrypt.Verify(model.CurrentPassword, admin.PasswordHash))
            {
                ModelState.AddModelError(nameof(model.CurrentPassword), "رمز عبور اشتباه است");
                return View(model);
            }

            if (BCrypt.Net.BCrypt.Verify(model.NewPassword, admin.PasswordHash))
            {
                ModelState.AddModelError(nameof(model.NewPassword), "رمز عبور جدید نباید با رمز عبور فعلی یکسان باشد");
                return View(model);
            }

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            admin.PasswordHash = newPasswordHash;

            if (admin.IsDefaultCredentials == true)
            {
                admin.IsDefaultCredentials = false;
            }

            await _context.SaveChangesAsync();

            await HttpContext.SignOutAsync("AdminAuth");
            TempData["Success"] = "رمز عبور با موفقیت تغییر یافت";
            return RedirectToAction("login", "AdminAccount");
        }
    }
}
