using System.ComponentModel.DataAnnotations;

namespace DiyorMarket.Domain.DTOs.Product
{
    public record ProductForCreateDto(
        string Name,
        string Description,
		decimal SalePrice,
		decimal SupplyPrice,
		DateTime ExpireDate,
        int CategoryId);
}   
