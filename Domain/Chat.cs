using System.Collections.Generic;

namespace Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
