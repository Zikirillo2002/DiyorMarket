namespace Lesson11.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalDue { get; set; }
        public Customer? Customer { get; set; }

        public virtual ICollection<SaleItem>? SaleItems { get; set; }
    }
}
