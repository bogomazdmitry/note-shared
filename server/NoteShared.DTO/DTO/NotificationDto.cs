namespace NoteShared.DTO.DTO
{
    public class NotificationDto
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public NotificationTypeDto Type { get; set; }
    }
}
