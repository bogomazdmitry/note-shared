using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;
using NoteShared.Infrastructure.Data.Entity.Users;
using System.ComponentModel.DataAnnotations;

namespace NoteShared.Infrastructure.Data.Entity.Notes
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        public int Order { get; set; }

        public int? NoteTextID { get; set; }

        public NoteText NoteText { get; set; }

        public int? DesignID { get; set; }

        public virtual NoteDesign NoteDesign { get; set; }

        public int? HistoryID { get; set; }

        public virtual NoteHistory NoteHistory { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }
    }
}
