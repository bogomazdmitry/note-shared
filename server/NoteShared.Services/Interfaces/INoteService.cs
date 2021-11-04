using NoteShared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public interface INoteService
    {
        Task<ServiceResponse<IEnumerable<NoteDto>>> GetAllNotes(string userID);

        Task<ServiceResponse<NoteDto>> GetNote(string userID, int noteId);

        Task<ServiceResponse<NoteDto>> CreateNote(string userID, NoteDto noteDto);

        Task<ServiceResponse> DeleteNote(string userID, int deleteNoteID);

        Task<ServiceResponse<NoteTextDto>> UpdateNoteText(string userID, NoteTextDto updateNoteTextDto);

        Task<ServiceResponse<NoteDto>> UpdateNote(string userID, NoteDto updateNoteDto);

        Task<ServiceResponse<NoteDesignDto>> UpdateNoteDesign(string userID, NoteDesignDto updateNoteDesignDto);

        Task<ServiceResponse<IEnumerable<NoteOrderDto>>> UpdateOrderNotes(string userID, IEnumerable<NoteOrderDto> noteOrderDtoList);

        Task<ServiceResponse<List<string>>> GetUserIDListByNoteTextID(string userID, int noteTextID);

        Task<ServiceResponse<List<string>>> GetUserEmailListByNoteTextID(string userID, int noteTextID);

        Task<ServiceResponse> CanAddSharedUser(string currentUserID, string sharedUserEmail, int noteTextID);

        Task<ServiceResponse> DeleteSharedUser(string currentUserID, string sharedUserEmail, int noteTextID);

        Task<ServiceResponse<NoteDto>> AcceptSharedNote(string userID, int noteTextID, int notificationID);

        Task<ServiceResponse> DeclineSharedNote(string userID, int noteTextID, int notificationID);

        Task<ServiceResponse<string>> GetOwnerID(string userID, int noteTextID);
    }
}
