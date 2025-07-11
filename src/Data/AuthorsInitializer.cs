using Aaron.Models.Entities;
using Newtonsoft.Json;

namespace Aaron.Data
{
    public static class AuthorsInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Authors.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedFiles/Authors");
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var json = File.ReadAllText(file);
                    var author = JsonConvert.DeserializeObject<Author>(json);
                    if (author != null)
                    {
                        context.Authors.Add(author);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
