using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TTRPG_Character_Builder.Models;
using TTRPG_Character_Builder.ViewModels;


namespace TTRPG_Character_Builder.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public IActionResult Index()
        {
            return View("Index");
        }



        // GET: /Home/Privacy (optional)
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
