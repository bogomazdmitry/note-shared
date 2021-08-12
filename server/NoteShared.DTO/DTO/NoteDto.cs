namespace NoteShared.DTO
{
    public class NoteDto
    {
        public int ID { get; set; }

        public int Order { get; set; }

        public string Tittle { get; set; }

        public string Text { get; set; }

        public NoteTextDto NoteText { get; set; }

        public NoteDesignDto NoteDesign { get; set; }

        public NoteHistoryDto NoteHistory { get; set; }
    }
}
