using Microsoft.AspNetCore.Mvc;
using CourseWork.Models.DTO;
using CourseWork.Repositories.Abstract;
using CourseWork.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            model = new RegistrationModel
            {
                Email = "bob@gmail.com",
                Username = "user123",
                Name = "User123",
                Password = "Bob@123",
                PasswordConfirm = "Bob@123",
                Role = "User"
            };
            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
