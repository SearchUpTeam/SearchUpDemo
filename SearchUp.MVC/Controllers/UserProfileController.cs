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
        private readonly IFollowingService _followingService;
        private readonly IInterestsService _interestsService;

        public UserProfileController(UserManager<User> userManager, IEventService eventService, IFollowingService followingService, IInterestsService interestsService)
        {
            _userManager = userManager;
            _eventService = eventService;
            _followingService = followingService;
            _interestsService = interestsService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = new UserProfileViewModel() { 
                Username = user.UserName, 
                About = user.About, 
                Avatars = user.Avatars, 
                Events = await _eventService.GetVisitedByUserAsync(user.Id),
                Interests = await _interestsService.GetUserInterestsAsync(user.Id),
                FollowersCount = await _followingService.CountFollowersAsync(user.Id),
                FollowingsCount =await _followingService.CountFollowingsAsync(user.Id)
            };
            return View(profile);
        }
    }
}
