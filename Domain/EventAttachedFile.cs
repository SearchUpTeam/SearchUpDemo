using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("EventFiles")]
    public class EventAttachedFile : File
    {
        [Required]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}