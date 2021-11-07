using Microsoft.AspNetCore.Mvc;

namespace SearchUp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "UserProfile");
            return View();
        }
    }
}
