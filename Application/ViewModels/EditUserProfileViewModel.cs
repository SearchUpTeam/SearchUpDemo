using Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class EditUserProfileViewModel
    {
        [Required]
        public string Username { get; set; }
        public string About { get; set; }
        public IFormFile NewAvatar { get; set; }
        public ICollection<Avatar> Avatars { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<InterestTag> UserInterests { get; set; }
        public IEnumerable<InterestTag> Interests { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The passwords doesn't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string NewPasswordConfirm { get; set; }

    }
}
