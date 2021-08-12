using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Linq;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNotes : BaseRepository<Note>, IRepositoryNotes
    {
        public RepositoryNotes(ApplicationContext context) 
            : base(context)
        {
        }

        public IQueryable<string> GetUsersID(int noteID)
        {
            return GetAllQueryable().Where(note => note.ID == noteID).Select(note => note.UserID);
        }

    }
}
