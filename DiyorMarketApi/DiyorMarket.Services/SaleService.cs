using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;
using DiyorMarket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiyorMarket.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly DiyorMarketDbContext _context;

        public SaleService(IMapper mapper, DiyorMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetSaleResponse GetSales(SaleResourceParameters saleResourceParameters)
        {
            var query = GetFiltrSaleResParameters(saleResourceParameters);

            var sales = query.ToPaginatedList(saleResourceParameters.PageSize, saleResourceParameters.PageNumber);

            //foreach (var sale in sales)
            //{
            //    sale.Customer = _context.Customers.FirstOrDefault(x => x.Id == sale.CustomerId);
            //}
            
            var saleDtos = _mapper.Map<List<SaleDto>>(sales);

            var paginatedResult =  new PaginatedList<SaleDto>(saleDtos, sales.TotalCount, sales.CurrentPage, sales.PageSize);

            var result = GetResponse(paginatedResult);

            return result;
        }

        public SaleDto? GetSaleById(int id)
        {
            var sale = _context.Sales.FirstOrDefault(x => x.Id == id);

            var saleDto = _mapper.Map<SaleDto>(sale);

            return saleDto;
        }

        public SaleDto CreateSale(SaleForCreateDto saleToCreate)
        {
            var saleEntity = _mapper.Map<Sale>(saleToCreate);

            _context.Sales.Add(saleEntity);
            _context.SaveChanges();

            var saleDto = _mapper.Map<SaleDto>(saleEntity);

            return saleDto;
        }

        public void UpdateSale(SaleForUpdateDto saleToUpdate)
        {
            var saleEntity = _mapper.Map<Sale>(saleToUpdate);

            _context.Sales.Update(saleEntity);
            _context.SaveChanges();
        }

        public void DeleteSale(int id)
        {
            var sale = _context.Sales.FirstOrDefault(x => x.Id == id);
            if (sale is not null)
            {
                _context.Sales.Remove(sale);
            }
            _context.SaveChanges();
        }



        private IQueryable<Sale> GetFiltrSaleResParameters(
            SaleResourceParameters saleResourceParameters)
        {
            var query = _context.Sales
                .Include(x => x.SaleItems)
                .IgnoreAutoIncludes()
                .AsQueryable();

            if (saleResourceParameters.CustomerId is not null)
            {
                query = query.Where(x => x.CustomerId == saleResourceParameters.CustomerId);
            }

            if (!string.IsNullOrWhiteSpace(saleResourceParameters.SearchString))
            {
                query = query.Include(s => s.Customer)
                    .Where(x => x.Customer.FirstName.ToLower()
                .Contains(saleResourceParameters.SearchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(saleResourceParameters.OrderBy))
            {
                query = saleResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "id" => query.OrderBy(x => x.Id),
                    "iddesc" => query.OrderByDescending(x => x.Id),
                    "expiredate" => query.OrderBy(x => x.SaleDate),
                    "expiredatedesc" => query.OrderByDescending(x => x.SaleDate),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            return query;
        }

        private GetSaleResponse GetResponse(PaginatedList<SaleDto> paginated)
        {
            var result = new GetSaleResponse()
            {
                Data = paginated.ToList(),
                HasNextPage = paginated.HasNext,
                HasPreviousPage = paginated.HasPrevious,
                PageNumber = paginated.CurrentPage,
                PageSize = paginated.PageSize,
                TotalPages = paginated.TotalPages
            };

            return result;
        }
    }
}
