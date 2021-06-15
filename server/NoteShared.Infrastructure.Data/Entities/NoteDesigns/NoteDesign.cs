using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NoteShared.Infrastructure.Data.Entity.NoteDesigns
{
    public class NoteDesign
    {
        public int ID { get; set; }

        public string Color { get; set; }

        [JsonIgnore]
        public virtual ICollection<Note> Notes { get; set; }
    }
}
