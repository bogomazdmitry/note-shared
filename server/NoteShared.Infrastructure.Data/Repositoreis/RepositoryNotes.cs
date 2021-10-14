using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace NoteShared.Infrastructure.Data.Repositories
{
    public class RepositoryNotes : BaseRepository<Note>, IRepositoryNotes
    {
        public RepositoryNotes(ApplicationContext context)
            : base(context)
        {
        }

        public async Task<bool> HasAccessForUser(int noteID, string userID)
        {
            return (await GetByAsync(note => note.ID == noteID))?.UserID == userID;
        }

        public List<int> GetNoteOrderList(string userID)
        {
            return GetAllByQueryable(note => note.UserID == userID)
                .Select(note => note.Order)
                .ToList();
        }

        public int GetMinimalNoteOrder(string userID)
        {
            return GetAllByQueryable(note => note.UserID == userID)
                .Select(note => note.Order)
                .Min();
        }

        public bool HasNote(string userID)
        {
            return GetAllByQueryable(note => note.UserID == userID).Any();
        }
    }
}
