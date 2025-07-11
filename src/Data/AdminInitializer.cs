using Aaron.Models.Entities;

namespace Aaron.Data
{
    public static class AdminInitializer
    {
        public static void SeedAdmin(AppDbContext context)
        {
            if (context.Admins.Any()) return;

            var admin = new Admin()
            {
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = "Admin"
            };

            context.Admins.Add(admin);

            context.SaveChanges();
        }
    }
}
