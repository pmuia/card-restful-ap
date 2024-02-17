using Microsoft.AspNetCore.Mvc;

namespace Cards.WEB.Areas.Messaging.Controllers
{
    public class EmailMessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
