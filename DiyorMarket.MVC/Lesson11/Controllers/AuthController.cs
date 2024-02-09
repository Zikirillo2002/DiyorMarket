using Lesson11.Models;
using Lesson11.Stores.User;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Http;
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

        // GET: AuthController
        public IActionResult Login()
        {
            return View();
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

        public IActionResult Register()
        {
            return View();
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





        // GET: AuthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
