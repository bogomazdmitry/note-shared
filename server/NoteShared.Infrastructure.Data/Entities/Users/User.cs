using Microsoft.AspNetCore.Identity;
using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Collections.Generic;

namespace NoteShared.Infrastructure.Data.Entity.Users
{
    public class User : IdentityUser
    {
        public virtual ICollection<Note> Notes { get; set; }
    }
}
