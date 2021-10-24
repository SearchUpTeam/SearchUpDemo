using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual ICollection<MessageAttachedFile> AttachedFiles { get; set; }
        [Required]
        public virtual Chat Chat { get; set; }
        public int ChatId { get; set; }
    }
}
