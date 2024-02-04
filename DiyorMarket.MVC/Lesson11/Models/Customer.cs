using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(Customer))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Sale>? Sales { get; set; }
    }
}
