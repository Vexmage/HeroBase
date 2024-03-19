using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TTRPG_Character_Builder.ViewModels;
using TTRPG_Character_Builder.Services;

namespace TTRPG_Character_Builder.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly RegistrationService _registrationService;

        public AccountController(AuthenticationService authenticationService, RegistrationService registrationService)
        {
            _authenticationService = authenticationService;
            _registrationService = registrationService;
        }

        // GET: Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model); // Redisplay form on failure
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _registrationService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model); // Redisplay form on failure
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
