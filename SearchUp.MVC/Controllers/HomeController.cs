using Microsoft.AspNetCore.Mvc;

namespace SearchUp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
