using AutoMapper;
using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Exceptions;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;
using DiyorMarket.Infrastructure.Persistence;

namespace DiyorMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DiyorMarketDbContext _context;

        public CategoryService(IMapper mapper,
            DiyorMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetCategoryResponse GetCategories(CategoryResourceParameters categoryResourceParameters)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryResourceParameters.SearchString))
            {
                query = query.Where(x => x.Name.Contains(categoryResourceParameters.SearchString));
            }

            if (!string.IsNullOrEmpty(categoryResourceParameters.OrderBy))
            {
                query = categoryResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "name" => query.OrderBy(x => x.Name),
                    "namedesc" => query.OrderByDescending(x => x.Name),
                    _ => query.OrderBy(x => x.Name),
                };
            }

            var categories = query.ToPaginatedList(categoryResourceParameters.PageSize, categoryResourceParameters.PageNumber);
            // var products = query.ToList();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            var paginatedResult = new PaginatedList<CategoryDto>(categoryDtos, categories.TotalCount, categories.CurrentPage, categories.PageSize);

            var result = new GetCategoryResponse()
            {
                Data = paginatedResult.ToList(),
                HasNextPage = paginatedResult.HasNext,
                HasPreviousPage = paginatedResult.HasPrevious,
                PageNumber = paginatedResult.CurrentPage,
                PageSize = paginatedResult.PageSize,
                TotalPages = paginatedResult.TotalPages
            };

            return result;
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
            {
                throw new EntityNotFoundException($"Category with id: {id} not found");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToCreate);

            _context.Categories.Add(categoryEntity);

            _context.SaveChanges();

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public void UpdateCategory(CategoryForUpdateDto categoryToUpdate)
        {
            var categoryEntity = _mapper.Map<Category>(categoryToUpdate);

            _context.Categories.Update(categoryEntity);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category is not null)
            {
                _context.Categories.Remove(category);
            }

            _context.SaveChanges();
        }
    }
}
