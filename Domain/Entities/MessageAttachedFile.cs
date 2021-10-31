using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("MessageFiles")]
    public class MessageAttachedFile : File
    {
        [Required]
        public Message Message { get; set; }
        public int MessageId { get; set; }
    }
}