using System.ComponentModel.DataAnnotations;

namespace NoteShared.DTO
{
    public class UserInfoResponse
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
