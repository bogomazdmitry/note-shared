namespace NoteShared.DTO.Request
{
    public class DeleteSharedUserRequest
    {
        public string Email { get; set; }

        public int NoteTextID { get; set; }
    }
}
