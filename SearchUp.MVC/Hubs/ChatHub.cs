using Microsoft.AspNetCore.SignalR;

namespace SearchUp.MVC.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() =>
            Context.ConnectionId;
    }
}
