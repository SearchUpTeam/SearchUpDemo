using Application.ViewModels;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Username, BirthDate = model.BirthDate };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                    
                    return RedirectToAction("Index", "UserProfile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            string res;
            Request.Cookies.TryGetValue("LastVisit", out res);            
            return View(new LoginViewModel() { Username = res, Password = null});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    DateTime now = DateTime.Now;
                    Response.Cookies.Append("LastVisit", model.Username, new CookieOptions
                    {
                        Expires = now.AddHours(1)
                    });
                    return RedirectToAction("Index", "UserProfile");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login and(or) password");
                }  
            }            

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
