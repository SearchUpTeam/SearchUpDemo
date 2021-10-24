using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MessageAttachedFile : File
    {
        [Required]
        public Message Message { get; set; }
        public int MessageId { get; set; }
    }
}