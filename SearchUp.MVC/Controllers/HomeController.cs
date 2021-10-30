using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;
using SearchUp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchUpContext _context;
        public HomeController(SearchUpContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            _context.SaveChanges();
            return View();
        }
    }
}
