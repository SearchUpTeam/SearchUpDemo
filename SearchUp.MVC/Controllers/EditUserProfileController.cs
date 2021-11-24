using Application.Interfaces;
using Application.ViewModels;
using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    [Authorize]
    public class EditUserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IInterestsService _interestsService;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public EditUserProfileController(
            UserManager<User> userManager,
            IFileService fileService,
            IInterestsService interestsService,
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _fileService = fileService;
            _interestsService = interestsService;
            _environment = environment;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var editProfile = new EditUserProfileViewModel
            {
                Username = user.UserName,
                About = user.About,
                Avatars = await _fileService.GetAvatarsAsync(user.Id),
                Email = user.Email,
                Interests = (ICollection<InterestTag>)await _interestsService.GetUserInterestsAsync(user.Id)
            };
            return View(editProfile);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChanges(EditUserProfileViewModel editProfile)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.UserName = editProfile.Username;
                user.About = editProfile.About;
                user.Email = editProfile.Email;
                if (editProfile.NewAvatar != null)
                {
                    var uniqueFileName = GetUniqueFileName(editProfile.NewAvatar.FileName);
                    var path = Path.Combine(
                        _environment.WebRootPath,
                        _configuration
                        .GetSection("FileStorages")
                        .GetSection("Avatars")
                        .Value,
                        uniqueFileName);
                    await editProfile.NewAvatar.CopyToAsync(new FileStream(path, FileMode.Create));
                    var avatar = new Avatar()
                    {
                        FileType = FileType.img,
                        Path = uniqueFileName,
                        UserId = user.Id
                    };
                    await _fileService.CreateAvatarAsync(avatar);
                }
                if (editProfile.OldPassword != null && editProfile.NewPassword != null)
                    await _userManager.ChangePasswordAsync(user, editProfile.OldPassword, editProfile.NewPassword);
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "UserProfile");
            }
            return RedirectToAction("Index");
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
