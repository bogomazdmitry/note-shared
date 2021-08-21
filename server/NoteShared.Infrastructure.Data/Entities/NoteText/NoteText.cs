using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoteShared.Infrastructure.Data.Entity.NoteTexts
{
    public class NoteText
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
    
        public string Text { get; set; }

        public IEnumerable<Note> Notes { get; set; }
    }
}
