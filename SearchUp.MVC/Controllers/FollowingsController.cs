using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly IFollowingService _followingService;
        public FollowingsController(IFollowingService followingService)
        {
            _followingService = followingService;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(int followerId, int followedId)
        {
            await _followingService.FollowAsync(followerId, followedId);
            return RedirectToAction("Index", "UserProfile", new { id = followedId } );
        }

        [HttpPost]
        public async Task<IActionResult> Unfollow(int followerId, int followedId)
        {
            await _followingService.UnfollowAsync(followerId, followedId);
            return RedirectToAction("Index", "UserProfile", new { id = followedId });
        }
    }
}
