using System.Linq;

namespace NoteShared.Infrastructure.Data.Entity.Notes
{
    public interface IRepositoryNotes : IBaseRepository<Note>
    {
        public IQueryable<string> GetUsersID(int noteID);
    }
}
