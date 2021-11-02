using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class User : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
        public ICollection<Avatar> Avatars { get; set; }
        public ICollection<InterestTag> Interests { get; set; }
        public ICollection<Following> Follows { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
