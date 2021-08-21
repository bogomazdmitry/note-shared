using System.Collections.Generic;

namespace NoteShared.Infrastructure.Data.Entity.NoteTexts
{
    public interface IRepositoryNoteTexts : IBaseRepository<NoteText>
    {
        List<string> GetUserIDList(int noteTextID);

        List<string> GetUserEmailList(int noteTextID);

        bool HasAccessForUser(int noteTextID, string userID);

    }
}
