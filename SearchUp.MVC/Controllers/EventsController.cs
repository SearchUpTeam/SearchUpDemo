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
using System.IO;

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
        public async Task<IActionResult> Index(int page = 1, string searchStr = ""){
            var viewModel = new EventSearchViewModel(){SearchStr=""};
            if (searchStr != ""){
                const int numOfResults = 10;
                viewModel.Events = await _eventService.GetBySearchRequestAsync(searchStr, skip: numOfResults*(page-1), take: numOfResults);
                viewModel.SearchStr = searchStr;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(EventSearchViewModel viewModel){
            return RedirectToAction("Index", new {viewModel.Page, viewModel.SearchStr});
        }

        [HttpGet]
        public async Task<IActionResult> EventProfile(int id)
        {
            Event eventObj = await _eventService.GetEventByIdAsync(id);
            var eventViewModel = new EventProfileViewModel(){
                Id = eventObj.Id,
                Title = eventObj.Title,
                Description = eventObj.Description,
                StartTime = eventObj.StartTime,
                EndTime = eventObj.EndTime,
                Participants = eventObj.memberships.Select(m => m.User).ToList(),
                Topics = eventObj.Topics,
                AttachedFiles = eventObj.AttachedFiles,
                ChatId = eventObj.ChatId
            };            
            return View(eventViewModel);
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

                // create chat for event
                Chat eventChat = new Chat{
                    Name = $"{eventViewModel.Title} chat",
                    ChatType = ChatType.Group
                };
                await _chatService.CreateAsync(eventChat, creator.Id);
                
                // create event object
                Event eventObj = new Event{
                    Title = eventViewModel.Title,
                    Description = eventViewModel.Description,
                    StartTime = eventViewModel.StartTime,
                    EndTime = eventViewModel.EndTime,
                    Topics = await _interestsService.GetInterestsById(eventViewModel.SelectedTopicsId),
                    ChatId = eventChat.Id
                };

                // upload files
                if(eventViewModel.Files != null){
                    string absoluteDir = Path.Combine(_appEnviroment.WebRootPath, "FileStorage");
                    string serverStorageDir = Path.Combine("/FileStorage");

                    string fileName, absolutePath, relativePath;
                    eventObj.AttachedFiles = new List<EventAttachedFile>();

                    foreach (var file in eventViewModel.Files)
                    {
                        fileName = Guid.NewGuid().ToString()+'_'+file.FileName;
                        absolutePath = Path.Combine(absoluteDir, fileName);
                        relativePath = Path.Combine(serverStorageDir, fileName);

                        using (var fileStream  = new FileStream(absolutePath, FileMode.Create)){
                            await file.CopyToAsync(fileStream);
                        }
                        // connect event with files
                        eventObj.AttachedFiles.Add(new EventAttachedFile(){Path = relativePath});   
                    }
                }

                await _eventService.CreateAsync(eventObj, creator.Id);
                return RedirectToAction("EventProfile", "Events", new {id = eventObj.Id});
            }
            return View("Error");
        }
    }
}