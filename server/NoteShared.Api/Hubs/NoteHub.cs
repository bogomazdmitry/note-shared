using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NoteShared.DTO;
using NoteShared.Services.Interfaces;
using System.Threading.Tasks;

namespace NoteShared.Api.Hubs
{
    [Authorize]
    public class NoteHub : Hub
    {
        private readonly INoteService _noteService;

        public NoteHub(INoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task UpdateNoteText(NoteTextDto noteDtoText)
        {
            var currentUser = Context.User;
            var currentUserID = currentUser.FindFirst(i => i.Type == JwtClaimTypes.Subject).Value;
            var serviceResponceUserIDList = await _noteService.GetUserIDListByNoteTextID(currentUserID, noteDtoText.ID);
            if(serviceResponceUserIDList.Success)
            {
                var userIDList = serviceResponceUserIDList.ModelRequest;
                userIDList.Remove(currentUserID);
                var result = await _noteService.UpdateNoteText(currentUserID, noteDtoText);
                await Clients.Users(userIDList).SendAsync("UpdateNoteText", noteDtoText);
            }
        }
    }
}
