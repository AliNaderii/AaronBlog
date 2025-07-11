using Microsoft.EntityFrameworkCore;

namespace Aaron.Models.ViewModels
{
    public class PaginatedList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(List<T> items, int pageNumber, int totalItems, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((totalItems / (double)pageSize));
            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, pageNumber, totalItems, pageSize);
        }
    }
}