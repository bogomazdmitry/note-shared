using NoteShared.Infrastructure.Data.Entity.Notes;
using System;

namespace NoteShared.Infrastructure.Data.Entity.NoteHistories
{
    public class NoteHistory
    {
        public int ID { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastChangesDateTime { get; set; }

        public int NoteID { get; set; }

        public virtual Note Note { get; set; }
    }
}
