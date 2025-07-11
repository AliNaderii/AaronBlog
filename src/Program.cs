using Microsoft.EntityFrameworkCore;
using Aaron.Data;
using Aaron.Middlewares;

namespace Aaron
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthentication("AdminAuth")
                .AddCookie("AdminAuth", options =>
                {
                    options.LoginPath = "/admin/login";
                    options.LogoutPath = "/admin/logout";
                    options.AccessDeniedPath = "/admin/access-denied";
                });

            var app = builder.Build();

            // Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
                AuthorsInitializer.Seed(db);
                CategoriesAndTagsInitializer.Seed(db);
                BooksInitializer.Seed(db);
            }

            // Seed Admin
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
                AdminInitializer.SeedAdmin(db);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<EnforceCredentialsChangeMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
