using Microsoft.AspNetCore.Mvc;

namespace TTRPG_Character_Builder.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
