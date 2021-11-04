using System.Threading.Tasks;

namespace NoteShared.Infrastructure.Data.Entity.Notes
{
    public interface IRepositoryNotes : IBaseRepository<Note>
    {
        int GetMinimalNoteOrder(string userID);

        Task<bool> HasAccessForUser(int noteID, string userID);

        bool HasNote(string userID);
    }
}
