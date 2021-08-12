using System.ComponentModel.DataAnnotations;

namespace NoteShared.DTO
{
    public class SignInUserRequest
    {
        [Required(ErrorMessage = "email require")]
        [EmailAddress(ErrorMessage = "email email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
