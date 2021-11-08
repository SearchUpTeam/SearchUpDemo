using Domain.Enums;
using System.Collections.Generic;

namespace Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType ChatType { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatMembership> memberships { get; set; }
    }
}
