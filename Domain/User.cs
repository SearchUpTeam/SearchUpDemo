using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
        public virtual ICollection<Avatar> Avatars { get; set; }
        [Required]
        public virtual ICollection<InterestTag> Interests { get; set; }
        public virtual ICollection<Following> Follows { get; set; }
        public virtual ICollection<Following> Followers { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserChat> Chats { get; set; }
    }
}
