using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(UserLogin))]
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string? Phone { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
