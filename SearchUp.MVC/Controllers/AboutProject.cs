using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    public class AboutProject : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
