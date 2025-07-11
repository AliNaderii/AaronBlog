using Aaron.Models.Entities;
using Newtonsoft.Json;

namespace Aaron.Data
{
    public static class BooksInitializer
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

                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedFiles/Books");
                var subDirectories = Directory.EnumerateDirectories(path);
                foreach (var dir in subDirectories)
                {
                    var files = Directory.GetFiles(dir);
                    foreach (var file in files)
                    {
                        var json = File.ReadAllText(file);
                        var book = JsonConvert.DeserializeObject<Book>(json);
                        if (book != null)
                        {
                            var authorInDb = context.Authors.FirstOrDefault(a => a.Name == book.Author.Name);
                            if (authorInDb != null)
                            {
                                book.AuthorId = authorInDb.Id;
                                book.Author = null;
                            }
                            else
                            {
                                context.Authors.Add(book.Author);
                            }

                            var categoryInDb = context.Categories.FirstOrDefault(c => c.Name == book.Category.Name);
                            if (categoryInDb != null)
                            {
                                book.CategoryId = categoryInDb.Id;
                                book.Category = null;
                            }
                            else
                            {
                                context.Categories.Add(book.Category);
                            }

                            var newTags = new List<Tag>();
                            foreach (var tag in book.Tags)
                            {
                                var tagInDb = context.Tags.FirstOrDefault(t => t.Name == tag.Name);
                                if (tagInDb != null)
                                {
                                    newTags.Add(tagInDb);
                                }
                                else
                                {
                                    context.Tags.Add(tag);
                                    newTags.Add(tag);
                                }
                            }

                            book.Tags.Clear();
                            book.Tags = newTags;
                            context.Books.Add(book);
                        }
                    }
                }
                context.SaveChanges();
            }

        }
    }
}