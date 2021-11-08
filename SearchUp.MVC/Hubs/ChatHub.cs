using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SearchUp.MVC.Hubs
{
    public class ChatHub : Hub
    {
        public Task JoinChat(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
        public Task LeaveChat(string chatId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
