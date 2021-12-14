using Application.ViewModels;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        private readonly IFileService _fileService;

        public UserProfileController(
            UserManager<User> userManager,
            IEventService eventService,
            IFollowingService followingService,
            IInterestsService interestsService,
            IFileService fileService)
        {
            _userManager = userManager;
            _eventService = eventService;
            _followingService = followingService;
            _interestsService = interestsService;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index(int id)
        {
            User user;
            if (id == 0)
                user = await _userManager.GetUserAsync(User);
            else
                user = await _userManager.FindByIdAsync(id.ToString());
            
            var profile = new UserProfileViewModel() { 
                Username = user.UserName, 
                About = user.About, 
                Avatars = await _fileService.GetAvatarsAsync(user.Id), 
                Events = await _eventService.GetVisitedByUserAsync(user.Id),
                Interests = await _interestsService.GetUserInterestsAsync(user.Id),
                FollowersCount = await _followingService.CountFollowersAsync(user.Id),
                FollowingsCount =await _followingService.CountFollowingsAsync(user.Id)
            };
            return View(profile);
        }
    }
}
