using NoteShared.Infrastructure.Data.Entity.Notes;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNotes : BaseRepository<Note>, IRepositoryNotes
    {
        public RepositoryNotes(ApplicationContext context) 
            : base(context)
        {
        }

    }
}
