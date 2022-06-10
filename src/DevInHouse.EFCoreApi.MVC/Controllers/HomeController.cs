using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevInHouse.EFCoreApi.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUsuarioApplicationService usuarioApplicationService)
        {
            _logger = logger;
            _usuarioApplicationService = usuarioApplicationService; 
        }

        public IActionResult Index()
        {
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