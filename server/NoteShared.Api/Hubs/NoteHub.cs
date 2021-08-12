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
        private readonly NoteService _noteService;

        public NoteHub(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task UpdateNoteText(NoteTextDto noteDtoText)
        {
            var currentUser = Context.User;
            var currentID = currentUser.FindFirst(i => i.Type == JwtClaimTypes.Subject).Value;
            var usersID = (await _noteService.GetUserIDsByNoteTextID(currentID, noteDtoText.ID)).ModelRequest;
            usersID.Remove(currentID);
            var result = await _noteService.UpdateNoteText(currentID, noteDtoText);
            await Clients.Users(usersID).SendAsync("UpdateNoteText", noteDtoText);
        }
    }
}
