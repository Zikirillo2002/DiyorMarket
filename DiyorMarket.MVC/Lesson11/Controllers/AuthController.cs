using Lesson11.Models;
using Lesson11.Stores.User;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserDataStore _userDataStore;
        public AuthController(IUserDataStore userDataStore)
        {
            _userDataStore = userDataStore ?? throw new ArgumentNullException(nameof(userDataStore));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = new UserLogin
            {
                Password = loginViewModel.Password,
                Login = loginViewModel.Login,
            };

            if (!_userDataStore.AuthenticateLogin(user).Item1)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ModelState.AddModelError("Password", "Incorrect password or login");
                return View(loginViewModel); // Вернуть представление с ошибками
            }

            return RedirectToAction("Index", "Dashboard");
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            if (registerViewModel.Password != registerViewModel.RepeatPassword)
            {
                ModelState.AddModelError(string.Empty, "The password does not match.");
                return BadRequest("The password does not match.");
            }

            var user = new UserLogin
            {
                Login = registerViewModel.Login,
                Password = registerViewModel.Password,
                FullName = registerViewModel.FullName,
                Phone = registerViewModel.Phone
            };

            if (!_userDataStore.RegisterLogin(user).Item1)
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                return BadRequest("Invalid register attempt.");
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
