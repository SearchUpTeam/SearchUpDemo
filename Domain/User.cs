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
        public ICollection<Avatar> Avatars { get; set; }
        [Required]
        public ICollection<InterestTag> Interests { get; set; }
        public ICollection<Following> Follows { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChat> Chats { get; set; }
    }
}
