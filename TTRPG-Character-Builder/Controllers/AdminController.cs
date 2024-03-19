// AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.ViewModels;
using TTRPG_Character_Builder.Services;

namespace TTRPG_Character_Builder.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        // GET: Admin
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListRoles));
        }

        // GET: Admin/ListRoles
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: Admin/CreateUser
        public IActionResult CreateUser()
        {
            return View();
        }

        // POST: Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Add the user to a default role, e.g., "RegisteredUser"
                    await _userManager.AddToRoleAsync(user, "RegisteredUser");
                    return RedirectToAction(nameof(ListUsers));
                }
                AddErrors(result);  // Ensure you have a method to add errors to ModelState
            }
            return View(model);
        }

        // GET: Admin/CreateRole
        public IActionResult CreateRole()
        {
            return View();
        }

        // POST: Admin/CreateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                AddErrors(result);
            }
            return View(roleName);
        }

        // GET: Admin/EditRole/id
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new RoleEditViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Members.Add(user);
                }
                else
                {
                    model.NonMembers.Add(user);
                }
            }
            return View(model);
        }

        // POST: Admin/EditRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role != null)
            {
                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                AddErrors(result);
            }
            else
            {
                return View("NotFound");
            }
            return View(model);
        }

        // GET: Admin/DeleteRole/id
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFound");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ListRoles));
            }
            AddErrors(result);
            return View("ListRoles");
        }

        // GET: Admin/ListUsers
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: Admin/SeeUsers
        public async Task<IActionResult> SeeUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
