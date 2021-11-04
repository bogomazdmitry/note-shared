using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NoteShared.DTO.DTO;
using System.Threading.Tasks;

namespace NoteShared.Api.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public NotificationsHub()
        { }

        public async Task SendNewNotification(string userID, NotificationDto notificationInfoDto)
        {
            await Clients.Client(userID).SendAsync("send-new-notification", notificationInfoDto);
        }

    }
}
