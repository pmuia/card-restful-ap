using Microsoft.AspNetCore.Mvc;

namespace Cards.WEB.Areas.Messaging.Controllers
{
    public class TextMessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
