using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SearchUp.MVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly UserManager<User> _userManager;
        public ChatHub(IChatService chatService, UserManager<User> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
        }
        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
        public async Task SendMessage(
            int chatId,
            string username,
            string text)
        {
            var user = await _userManager.FindByNameAsync(username);
            var message = new Message() { ChatId = chatId, Text = text, SenderId = user.Id, Timestamp = DateTime.Now };
            await Clients.Group(chatId.ToString())
                .SendAsync("ReceiveMessage",
                new
                {
                    sender = user.UserName,
                    text = message.Text,
                    timestamp = message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                }) ;
            await _chatService.CreateMessage(message);
        }
    }
}
