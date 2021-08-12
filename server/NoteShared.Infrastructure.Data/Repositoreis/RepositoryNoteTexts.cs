using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;
using System.Linq;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNoteTexts : BaseRepository<NoteText>, IRepositoryNoteTexts
    {
        public RepositoryNoteTexts(ApplicationContext context) 
            : base(context)
        {
        }

        public IQueryable<string> GetUserIDsAsQueryable(int noteTextID)
        {
            return _context.Set<Note>().Where(el => el.NoteTextID == noteTextID).Select(e => e.UserID);
        }

        public IQueryable<string> GetUserEmailsAsQueryable(int noteTextID)
        {
            return _context.Set<Note>().Where(el => el.NoteTextID == noteTextID).Select(e => e.User.Email);
        }
    }
}
