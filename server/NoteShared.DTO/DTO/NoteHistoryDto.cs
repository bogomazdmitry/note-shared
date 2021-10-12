using System;

namespace NoteShared.DTO
{
    public class NoteHistoryDto
    {
        public int ID { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastChangesDateTime { get; set; }
    }
}
