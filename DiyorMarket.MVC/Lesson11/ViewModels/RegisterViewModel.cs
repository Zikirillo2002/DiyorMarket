using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lesson11.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }  
    }
}
