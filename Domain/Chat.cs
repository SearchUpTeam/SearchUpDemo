using System.Collections.Generic;

namespace Domain
{
    public abstract class Chat
    {
        public int Id { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
