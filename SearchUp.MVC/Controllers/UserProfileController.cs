using Application.ViewModels;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SearchUp.MVC.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        public IActionResult Index()
        {
            var profile = new UserProfileViewModel() { Username=_userManager}
            return View();
        }
    }
}
