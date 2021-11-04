using NoteShared.Infrastructure.Data.Entities.Notifications;
using NoteShared.Infrastructure.Data.Repositories;

namespace NoteShared.Infrastructure.Data.Repositoreis
{
    public class RepositoryNotifications : BaseRepository<Notification>, IRepositoryNotifications
    {
        public RepositoryNotifications(ApplicationContext context)
            : base(context)
        {
        }
    }
}
