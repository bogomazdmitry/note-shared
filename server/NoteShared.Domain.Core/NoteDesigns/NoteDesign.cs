using NoteShared.Domain.Core.Notes;
using System.Collections.Generic;

namespace NoteShared.Domain.Core.NoteDesigns
{
    public class NoteDesign
    {
        public int ID { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
