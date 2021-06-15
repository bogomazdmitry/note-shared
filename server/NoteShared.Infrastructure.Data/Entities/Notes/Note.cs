using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoteShared.Infrastructure.Data.Entity.Notes
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        public int Order { get; set; }

        public string Tittle { get; set; }
    
        public string Text { get; set; }

        public int? DesignID { get; set; }

        public virtual NoteDesign Design { get; set; }

        public int? HistoryID { get; set; }

        public virtual NoteHistory NoteHistory { get; set; }

        public string UserID { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
