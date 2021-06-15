using NoteShared.Domain.Core.NoteDesigns;
using NoteShared.Domain.Core.NoteHistories;
using NoteShared.Domain.Core.Users;

namespace NoteShared.Domain.Core.Notes
{
    public class Note
    {
        public int ID { get; set; }

        public string Tittle { get; set; }
    
        public string Text { get; set; }

        public int DesignID { get; set; }

        public virtual NoteDesign Design { get; set; }

        public int HistoryID { get; set; }

        public virtual NoteHistory History { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }
    }
}
