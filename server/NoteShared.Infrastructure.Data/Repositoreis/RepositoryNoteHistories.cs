using NoteShared.Infrastructure.Data.Entity.NoteHistories;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNoteHistories : BaseRepository<NoteHistory>, IRepositoryNoteHistories
    {
        public RepositoryNoteHistories(ApplicationContext context)
            : base(context)
        {
        }
    }
}
