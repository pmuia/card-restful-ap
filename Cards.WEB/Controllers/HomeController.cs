using Microsoft.AspNetCore.Mvc;
using Cards.WEB.Models;
using System.Diagnostics;

namespace Cards.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}