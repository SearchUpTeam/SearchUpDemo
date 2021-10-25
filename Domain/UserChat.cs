using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("UserChats")]
    public class UserChat : Chat
    {
        public virtual ICollection<User> Participants { get; set; }
    }
}
