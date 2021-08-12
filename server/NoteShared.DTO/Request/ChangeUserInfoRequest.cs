using System.ComponentModel.DataAnnotations;

namespace NoteShared.DTO
{
    public class ChangeUserInfoRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
