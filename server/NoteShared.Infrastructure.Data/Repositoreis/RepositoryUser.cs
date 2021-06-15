using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryUser : BaseRepository<User>, IRepositioryUser
    {
        public RepositoryUser(ApplicationContext context)
            : base(context)
        {
        }

    }
}
