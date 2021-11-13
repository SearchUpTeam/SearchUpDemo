using Application.Interfaces;
using Application.ViewModels;
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
        private readonly IHubContext<ChatHub> _chat;
        public MyChatsController(
            IChatService chatService,
            UserManager<User> userManager,
            IHubContext<ChatHub> chat)
        {
            _chatService = chatService;
            _userManager = userManager;
            _chat = chat;
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
            var user = await _userManager.GetUserAsync(User);
            var chatsViewModel = new ChatsViewModel()
            {
                Chats = await _chatService.GetChatsAsync(user.Id),
                CurrentChat = chat
            };
            return View(chatsViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> JoinChat(int chatId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _chatService.JoinChat(chatId, user.Id);
            return RedirectToAction("Chat", "MyChats", new { id = chatId });
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(
            int chatId,
            string message)
        {
            var user = await _userManager.GetUserAsync(User);
            var Message = new Message()
            {
                ChatId = chatId,
                Text = message,
                Timestamp = DateTime.UtcNow,
                SenderId = user.Id
            };
            await _chatService.CreateMessage(Message);
            return RedirectToAction("Chat", new { id = chatId });
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
