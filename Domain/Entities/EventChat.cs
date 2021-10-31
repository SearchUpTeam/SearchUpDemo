using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("EventChats")]
    public class EventChat : Chat
    {
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
