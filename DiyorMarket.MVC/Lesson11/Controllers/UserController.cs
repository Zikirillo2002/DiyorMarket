using Lesson11.Models;
using Lesson11.Stores.User;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDataStore _userDataStore;
        public UserController(IUserDataStore userDataStore)
        {
            _userDataStore = userDataStore ?? throw new ArgumentNullException(nameof(userDataStore));
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new UserLogin
            {
                Password = loginViewModel.Password,
                Login = loginViewModel.Login,
            };

            if (!_userDataStore.AuthenticateLogin(user))
            {
                return BadRequest("Invalid login attempt.");
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new UserLogin
            {
                Login = registerViewModel.Login,
                Password = registerViewModel.Password,
                FullName = registerViewModel.Name,
                Phone = registerViewModel.Phone
            };

            if (!_userDataStore.RegisterLogin(user))
            {
                return BadRequest("Invalid register attempt.");
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
