using NoteShared.Infrastructure.Data.Entity.NoteDesigns;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNoteDesigns : BaseRepository<NoteDesign>, IRepositoryNoteDesigns
    {
        public RepositoryNoteDesigns(ApplicationContext context)
            : base(context)
        {
        }

    }
}
