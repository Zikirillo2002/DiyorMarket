using Lesson11.Models;

namespace Lesson11.Response
{
    public class CategoryResponse
    {
        public IEnumerable<Category> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPages { get; set; }
    }
}
