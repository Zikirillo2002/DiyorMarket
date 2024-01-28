using DiyorMarket.Domain.DTOs.Category;

namespace DiyorMarket.Domain.Responses
{
    public class GetCategoriesResponse
    {
        public IEnumerable<CategoryDto> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPages { get; set; }
    }
}
