using MassTransit_RabbitMQ_2FA_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MassTransit_RabbitMQ_2FA_Authentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(DataModel mail)
        {
            ViewBag.Mail = mail;
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
