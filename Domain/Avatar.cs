using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Avatar : File
    {
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
