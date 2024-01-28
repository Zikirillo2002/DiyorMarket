using AutoMapper;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;
using DiyorMarket.Infrastructure.Persistence;
using DiyorMarket.ResourceParameters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly DiyorMarketDbContext _context;

        public SupplierService(IMapper mapper, DiyorMarketDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetSupplierResponse GetSuppliers(SupplierResourceParameters supplierResourceParameters)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(supplierResourceParameters.SearchString))
            {
                query = query.Where(x => x.FirstName.Contains(supplierResourceParameters.SearchString)
                || x.LastName.Contains(supplierResourceParameters.SearchString)
                || x.PhoneNumber.Contains(supplierResourceParameters.SearchString)
                || x.Company.Contains(supplierResourceParameters.SearchString));
            }

            if (!string.IsNullOrEmpty(supplierResourceParameters.OrderBy))
            {
                query = supplierResourceParameters.OrderBy.ToLowerInvariant() switch
                {
                    "firstname" => query.OrderBy(x => x.FirstName),
                    "firstnamedesc" => query.OrderByDescending(x => x.FirstName),
                    "lastname" => query.OrderBy(x => x.LastName),
                    "lastnamedesc" => query.OrderByDescending(x => x.LastName),
                    "phonenumber" => query.OrderBy(x => x.PhoneNumber),
                    "phonenumberdesc" => query.OrderByDescending(x => x.PhoneNumber),
                    "company" => query.OrderBy(x => x.Company),
                    "companydesc" => query.OrderByDescending(x => x.Company),
                    _ => query.OrderBy(x => x.FirstName),
                };
            }

            var suppliers = query.ToPaginatedList(supplierResourceParameters.PageSize, supplierResourceParameters.PageNumber);

            var supplierDtos = _mapper.Map<List<SupplierDto>>(suppliers);

            var paginatedResult = new PaginatedList<SupplierDto>(supplierDtos, suppliers.TotalCount, suppliers.CurrentPage, suppliers.PageSize);

            var result = new GetSupplierResponse()
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

        public SupplierDto? GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);

            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            return supplierDto;
        }

        public SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToCreate);

            _context.Suppliers.Add(supplierEntity);
            _context.SaveChanges();

            var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);

            return supplierDto;
        }

        public void UpdateSupplier(SupplierForUpdateDto supplierToUpdate)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierToUpdate);

            _context.Suppliers.Update(supplierEntity);
            _context.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
            }
            _context.SaveChanges();
        }
    }
}
