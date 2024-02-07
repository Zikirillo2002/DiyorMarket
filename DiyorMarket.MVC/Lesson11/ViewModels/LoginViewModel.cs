using System.ComponentModel.DataAnnotations;

namespace Lesson11.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
