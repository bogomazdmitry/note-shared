using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;
using System.Collections.Generic;
using System.Linq;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNoteTexts : BaseRepository<NoteText>, IRepositoryNoteTexts
    {
        public RepositoryNoteTexts(ApplicationContext context)
            : base(context)
        {
        }

        public List<string> GetUserIDList(int noteTextID)
        {
            return _context
                .Set<Note>()
                .Where(note => note.NoteTextID == noteTextID)
                .Select(note => note.UserID)
                .ToList();
        }

        public List<string> GetUserEmailList(int noteTextID)
        {
            return _context
                .Set<Note>()
                .Where(note => note.NoteTextID == noteTextID)
                .Select(note => note.User.Email)
                .ToList();
        }

        public bool HasAccessForUser(int noteTextID, string userID)
        {
            return _context
                .Set<Note>()
                .Where(note => note.NoteTextID == noteTextID)
                .Select(note => note.UserID)
                .Contains(userID);
        }
    }
}
