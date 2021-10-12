using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryUsers : BaseRepository<User>, IRepositioryUsers
    {
        public RepositoryUsers(ApplicationContext context)
            : base(context)
        {
        }

    }
}
