using Application.ViewModels;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;

namespace SearchUp.MVC.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventService _eventService;

        public UserProfileController(UserManager<User> userManager, IEventService eventService)
        {
            _userManager = userManager;
            _eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = new UserProfileViewModel() { Username = user.UserName, About = user.About, Avatars = user.Avatars, Events = await _eventService.GetVisitedByUserAsync(user.Id)};
            return View(profile);
        }
    }
}
