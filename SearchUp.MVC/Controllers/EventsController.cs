using Application.ViewModels;
using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using System;

namespace SearchUp.MVC.Controllers
{
    public class EventsController : Controller
    {
        readonly IEventService _eventService;
        readonly UserManager<User> _userManager;
        readonly IInterestsService _interestsService;
        readonly IChatService _chatService;
        readonly IWebHostEnvironment _appEnviroment;
        
        public EventsController(IEventService eventService, UserManager<User> userManager, IInterestsService interestsService, IChatService chatService, IWebHostEnvironment appEnvironment)

        {
            _eventService = eventService;
            _userManager = userManager;
            _interestsService = interestsService;
            _chatService = chatService;
            _appEnviroment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> EventProfile(int eventId)
        {
            var eventObj = _eventService.GetEventByIdAsync(eventId);
            var eventViewModel = new EventProfileViewModel();
            eventViewModel.AttachedFiles = eventObj.Result.AttachedFiles;
            eventViewModel.ChatId = eventObj.Result.ChatId;
            eventViewModel.Description = eventObj.Result.Description;
            eventViewModel.StartTime = eventObj.Result.StartTime;
            eventViewModel.EndTime = eventObj.Result.EndTime;
            eventViewModel.Memberships = eventObj.Result.memberships;
            eventViewModel.Title = eventObj.Result.Title;
            eventViewModel.Topics = eventObj.Result.Topics;
            return View(eventViewModel);
            //throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel eventViewModel)
        {
            if (ModelState.IsValid){
                var creator = await _userManager.GetUserAsync(User);

                Chat eventChat = new Chat{
                    Name = $"{eventViewModel.Title} chat",
                    ChatType = ChatType.Group
                };
                await _chatService.CreateAsync(eventChat, creator.Id);

                Event eventObj = new Event{
                    Title = eventViewModel.Title,
                    Description = eventViewModel.Description,
                    StartTime = eventViewModel.StartTime,
                    EndTime = eventViewModel.EndTime,
                    Topics = await _interestsService.GetInterestsById(eventViewModel.SelectedTopicsId),
                    AttachedFiles = eventViewModel.Files,
                    ChatId = eventChat.Id
                };
                await _eventService.CreateAsync(eventObj, creator.Id);
                //return RedirectToAction("EventProfile");
            }
            return View("Error");
        }
    }
}