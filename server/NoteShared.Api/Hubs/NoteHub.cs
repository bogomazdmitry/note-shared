using Microsoft.AspNetCore.SignalR;
using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Threading.Tasks;

namespace NoteShared.Api.Hubs
{
    public class NoteHub : Hub
    {
        public Task UpdateNote(string text, string tittle)
        {
            return Clients.All.SendAsync("UpdateNoteResponse", text, tittle);
        }
    }
}
