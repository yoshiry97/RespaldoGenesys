using Microsoft.AspNetCore.Authorization;
using Genesys.Modelos.ErrorViewModels;
using Genesys.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Genesys.Areas.Directorio.Controllers
{
    [Area("Directorio")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated )
                return Redirect("/Identity/Account/Login");

            if (User.Identity.IsAuthenticated && User.IsInRole("Gerente"))
                return View("Gerente");

            if (User.Identity.IsAuthenticated && User.IsInRole("Auxiliar"))
                return View("Auxiliar");

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