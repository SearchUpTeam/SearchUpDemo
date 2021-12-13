using Application.ViewModels;
using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using System.Text.Json;

namespace SearchUp.MVC.Controllers
{
    public class InterestsController: Controller
    {
        readonly IInterestsService _interestsService;
        public InterestsController(IInterestsService interestsService)
        {
            _interestsService = interestsService;
        }
        public async Task<IActionResult> GetInterestsBySubstring(string searchStr, int maxNumOfResults)
        {
            var interestTags = await _interestsService.GetInterestsBySubstringAsync(searchStr, maxNumOfResults);
            string result =  JsonSerializer.Serialize(interestTags);
            return Json(result);
        } 
    }
}