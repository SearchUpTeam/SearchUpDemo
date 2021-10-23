using System.Collections.Generic;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<File> AttachedFiles { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
