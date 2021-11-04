using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.Infrastructure.Data.Entities.Notifications
{
    public class Notification
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public string UserID { get; set; }

        public User User { get; set; }
    }
}
