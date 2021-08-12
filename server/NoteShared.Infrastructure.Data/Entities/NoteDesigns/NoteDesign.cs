using NoteShared.Infrastructure.Data.Entity.Notes;

namespace NoteShared.Infrastructure.Data.Entity.NoteDesigns
{
    public class NoteDesign
    {
        public int ID { get; set; }

        public string Color { get; set; }

        public int NoteID { get; set; }

        public virtual Note Note { get; set; }
    }
}
