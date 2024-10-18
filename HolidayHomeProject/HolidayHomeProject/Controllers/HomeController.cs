using System.Diagnostics;
using HolidayHomeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolidayHomeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Retrieve IsHost from session
           /* var isHost = HttpContext.Session.GetString("IsHost");

            // Pass it to the view using ViewData or ViewBag
            ViewBag.IsHost = isHost == "True"; // You can convert it to boolean if necessary
*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
