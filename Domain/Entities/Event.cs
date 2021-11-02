using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<User> Participants { get; set; }
        public ICollection<InterestTag> Topics { get; set; }
        public ICollection<EventAttachedFile> AttachedFiles { get; set; }
        public int ChatId { get; set; }
    }
}
