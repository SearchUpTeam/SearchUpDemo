using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class EventAttachedFile : File
    {
        [Required]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}