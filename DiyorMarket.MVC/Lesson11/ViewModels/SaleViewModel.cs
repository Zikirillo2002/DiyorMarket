using Lesson11.Models;

namespace Lesson11.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime SaleDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
