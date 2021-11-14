using Application.Interfaces;
using Application.ViewModels;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SearchUp.MVC.Controllers
{
    [Authorize]
    public class MyChatsController : Controller
    {
        private readonly IChatService _chatService;
        private readonly UserManager<User> _userManager;
        public MyChatsController(
            IChatService chatService,
            UserManager<User> userManager)
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
        public async Task<IActionResult> CreateChat(string name)
        {
            var chat = new Chat() { Name = name, ChatType = Domain.Enums.ChatType.Group };
            var user = await _userManager.GetUserAsync(User);
            await _chatService.CreateAsync(chat, user.Id);
            var chats = await _chatService.GetChatsAsync(user.Id);
            return RedirectToAction("Chat", "MyChats", new { id = chat.Id });
        }
        [HttpGet]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);
            if (!await _chatService.IsUserInChat(user.Id, chat.Id))
                return RedirectToAction("Index", "MyChats");
            var chatsViewModel = new ChatsViewModel()
            {
                Chats = await _chatService.GetChatsAsync(user.Id),
                CurrentChat = chat
            };
            return View(chatsViewModel);
        }
        public async Task<IActionResult> JoinChat(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            await _chatService.JoinChat(id, user.Id);
            return RedirectToAction("Chat", "MyChats", new { id = id });
        }
        public async Task<IActionResult> LeaveChat(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            await _chatService.LeaveChat(id, user.Id);
            return RedirectToAction("Index", "MyChats");
        }
    }
}
