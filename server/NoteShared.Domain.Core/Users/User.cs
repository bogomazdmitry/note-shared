using Microsoft.AspNetCore.Identity;
using NoteShared.Domain.Core.Notes;
using System.Collections.Generic;

namespace NoteShared.Domain.Core.Users
{
    public class User : IdentityUser
    {
        public virtual ICollection<Note> Notes { get; set; }
    }
}
