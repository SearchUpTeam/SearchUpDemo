using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class User : IdentityUser
    {
        public ICollection<Avatar> Avatars { get; set; }
        public ICollection<InterestTag> Interests { get; set; }
        public ICollection<Following> Follows { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChat> Chats { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
    }
}
