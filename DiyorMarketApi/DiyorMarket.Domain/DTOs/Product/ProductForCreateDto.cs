using System.ComponentModel.DataAnnotations;

namespace DiyorMarket.Domain.DTOs.Product
{
    public record ProductForCreateDto(
        [Required][MaxLength(5)] string Name,
        string Description,
        decimal Price,
        DateTime ExpireDate,
        int CategoryId);
}
