using Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class ChatsViewModel
    {
        public IEnumerable<Chat>Chats { get; set; }
        public Chat CurrentChat { get; set; }
    }
}
