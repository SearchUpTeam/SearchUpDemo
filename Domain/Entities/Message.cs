using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }
        public ICollection<MessageAttachedFile> AttachedFiles { get; set; }
        [Required]
        public Chat Chat { get; set; }
        public int ChatId { get; set; }
    }
}
