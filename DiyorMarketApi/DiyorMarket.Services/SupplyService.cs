using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.DTOs.Supply;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace DiyorMarket.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IMapper _mapper;
        private readonly DiyorMarketDbContext _context;

        public SupplyService(IMapper mapper, DiyorMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PaginatedList<SupplyDto> GetSupplies(SupplyResourceParameters supplyResourceParameters)
        {
            var query = _context.Supplies.AsQueryable();

            if (supplyResourceParameters.SupplierId is not null)
            {
                query = query.Where(x => x.SupplierId == supplyResourceParameters.SupplierId);
            }

            if (!string.IsNullOrEmpty(supplyResourceParameters.OrderBy))
            {
                query = supplyResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "id" => query.OrderBy(x => x.Id),
                    "iddesc" => query.OrderByDescending(x => x.Id),
                    "supplydate" => query.OrderBy(x => x.SupplyDate),
                    "supplydatedesc" => query.OrderByDescending(x => x.SupplyDate),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var supplies = query.ToPaginatedList(supplyResourceParameters.PageSize, supplyResourceParameters.PageNumber);

            var supplyDtos = _mapper.Map<List<SupplyDto>>(supplies);

            return new PaginatedList<SupplyDto>(supplyDtos, supplies.TotalCount, supplies.CurrentPage, supplies.PageSize);
        }

        public SupplyDto? GetSupplyById(int id)
        {
            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);

            var supplyDto = _mapper.Map<SupplyDto>(supply);

            return supplyDto;
        }

        public SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate)
        {
            var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

            _context.Supplies.Add(supplyEntity);
            _context.SaveChanges();

            var supplyDto = _mapper.Map<SupplyDto>(supplyEntity);

            return supplyDto;
        }

        public void UpdateSupply(SupplyForUpdateDto supplyToUpdate)
        {
            var supplyEntity = _mapper.Map<Supply>(supplyToUpdate);

            _context.Supplies.Update(supplyEntity);
            _context.SaveChanges();
        }

        public void DeleteSupply(int id)
        {
            var supply = _context.Supplies.FirstOrDefault(x => x.Id == id);
            if (supply is not null)
            {
                _context.Supplies.Remove(supply);
            }
            _context.SaveChanges();
        }
    }
}
