using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //[RegularExpression()]
        public string OldPassword { get; set; }

        //[RegularExpression()]
        public string NewPassword { get; set; }

        [Compare("Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
