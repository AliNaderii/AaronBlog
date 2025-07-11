using Aaron.Models.Entities;
using Newtonsoft.Json;

namespace Aaron.Data
{
    public static class CategoriesAndTagsInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedFiles/Categories/categories.json");
                var json = File.ReadAllText(path);
                var categories = JsonConvert.DeserializeObject<string[]>(json);
                if (categories != null)
                {
                    foreach (var c in categories)
                    {
                        var category = new Category()
                        {
                            Name = c
                        };
                        context.Categories.Add(category);
                    }
                }
            }

            if (!context.Tags.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedFiles/Tags/tags.json");
                var json = File.ReadAllText(path);
                var tags = JsonConvert.DeserializeObject<string[]>(json);
                if (tags != null)
                {
                    foreach (var item in tags)
                    {
                        var tag = new Tag()
                        {
                            Name = item
                        };
                        context.Tags.Add(tag);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
