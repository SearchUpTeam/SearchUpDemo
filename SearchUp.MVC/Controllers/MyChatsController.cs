using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SearchUp.MVC.Hubs;
using System;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    [Authorize]
    public class MyChatsController : Controller
    {
        private readonly IChatService _chatService;
        private readonly UserManager<User> _userManager;
        public MyChatsController(IChatService chatService, UserManager<User> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var chats = await _chatService.GetChatsAsync(user.Id);
            return View(chats);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            var chat = new Chat() { Name = name, ChatType = Domain.Enums.ChatType.Group };
            var user = await _userManager.GetUserAsync(User);
            await _chatService.CreateAsync(chat, user.Id);
            var chats = await _chatService.GetChatsAsync(user.Id);
            return RedirectToAction("Index", "MyChats", chats, name);
        }
        [HttpGet]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            return View(chat);
        }
        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            await _chatService.JoinChat(id, user.Id);
            return RedirectToAction("Chat", "MyChats", new { id = id });
        }
        public async Task<IActionResult> SendMessage(
            int chatId,
            string text,
            [FromServices] IHubContext<ChatHub> chat)
        {
            var user = await _userManager.GetUserAsync(User);
            var message = new Message() { ChatId = chatId, Text = text, SenderId = user.Id, Timestamp=DateTime.UtcNow };
            await _chatService.CreateMessage(message);
            await chat.Clients.Group(chatId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = message.Text,
                    Name = user.UserName,
                    Timestamp = message.Timestamp
                });
            return Ok();
        }
    }
}
