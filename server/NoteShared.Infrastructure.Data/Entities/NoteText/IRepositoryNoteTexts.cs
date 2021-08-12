using System.Linq;

namespace NoteShared.Infrastructure.Data.Entity.NoteTexts
{
    public interface IRepositoryNoteTexts : IBaseRepository<NoteText>
    {
        IQueryable<string> GetUserIDsAsQueryable(int noteTextID);

        IQueryable<string> GetUserEmailsAsQueryable(int noteTextID);

    }
}
