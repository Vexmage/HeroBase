using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Character_Builder.Data;
using TTRPG_Character_Builder.Models;

namespace TTRPG_Character_Builder.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Logic to display user profile or redirect to login
            return View();
        }

        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml", new RegisterViewModel());
        }

        public IActionResult Manage()
        {
            // You will need to get the current user's information here
            var viewModel = new ManageViewModel();
            // Populate viewModel with the current user's data
            return View(viewModel);
        }

        public IActionResult Login()
        {
            var users = _context.Users.ToList(); // Get the list of users
            ViewBag.Users = users; // Pass the list to the view using ViewBag
            return View("~/Views/Account/Login.cshtml", new LoginViewModel());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        private int? GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = viewModel.Username,
                    Password = HashPassword(viewModel.Password), // Hash the password
                    Email = viewModel.Email,
                    DateJoined = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(viewModel);
        }


        private string HashPassword(string password)
        {
            // Implement a secure password hashing mechanism
            // This is a placeholder - do not use this in production
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return with validation errors
            }

            var hashedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(viewModel.Password));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == viewModel.Username && u.Password == hashedPassword);
            if (user != null)
            {
                // User found with matching credentials
                HttpContext.Session.SetString("Username", user.Username);

                return RedirectToAction("Index", "Home");
            }

            // Invalid credentials
            ModelState.AddModelError("", "Invalid username or password");
            return View(viewModel); // Stay on the login page
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Example: Assuming you fetch the current user's ID from session or authentication context
                var currentUserId = GetCurrentUserId();

                if (!currentUserId.HasValue)
                {
                    return RedirectToAction("Login");
                }

                var currentUser = await _context.Users.FindAsync(currentUserId.Value);

                if (currentUser == null)
                {
                    return NotFound();
                }

                currentUser.Email = viewModel.Email; 

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }



    }
}
