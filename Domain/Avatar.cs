using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("Avatars")]
    public class Avatar : File
    {
        [Required]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
