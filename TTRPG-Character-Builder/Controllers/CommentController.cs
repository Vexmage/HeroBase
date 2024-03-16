using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.Data; // Ensure you're using the correct namespace for your context
using System.Threading.Tasks;
using System;

namespace TTRPG_Character_Builder.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context; // Your database context
        private readonly UserManager<ApplicationUser> _userManager; // For accessing user info

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Assuming you might want to list comments somewhere
        public IActionResult Index()
        {
            // You would retrieve and return a list of comments here
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Ensure only logged-in users can post comments
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = model.Content,
                    ApplicationUserId = _userManager.GetUserId(User),
                    CharacterId = model.CharacterId,
                    DatePosted = DateTime.UtcNow
                };

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Detail", "Character", new { id = model.CharacterId });
            }
            // If there are issues with the model, you should ideally redirect back to the character detail page with the comment section visible.
            // You might need to pass back error messages or set up a system to display validation errors on the redirected page.
            return RedirectToAction("Detail", "Character", new { id = model.CharacterId });
        }
    }
}
