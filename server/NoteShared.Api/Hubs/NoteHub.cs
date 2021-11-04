using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NoteShared.DTO;
using NoteShared.DTO.Request;
using NoteShared.Infrastructure.Data.Entity.Users;
using NoteShared.Services.Interfaces;
using System.Threading.Tasks;

namespace NoteShared.Api.Hubs
{
    [Authorize]
    public class NoteHub : Hub
    {
        private readonly INoteService _noteService;

        private readonly INotificationsService _notificationService;

        private readonly IHubContext<NotificationsHub> _hubContext;

        private readonly IRepositioryUsers _repositoryUsers;

        public NoteHub(
            INoteService noteService, 
            INotificationsService notificationsService, 
            IHubContext<NotificationsHub> hubContext, 
            IRepositioryUsers repositoryUsers
        ) {
            _noteService = noteService;
            _notificationService = notificationsService;
            _hubContext = hubContext;
            _repositoryUsers = repositoryUsers;
        }

        [HubMethodName("update-note-text")]
        public async Task UpdateNoteText(NoteTextDto noteDtoText)
        {
            var serviceResponceUserIDList = await _noteService.GetUserIDListByNoteTextID(LoggedInUserUserId, noteDtoText.ID);
            if (serviceResponceUserIDList.Success)
            {
                var userIDList = serviceResponceUserIDList.ModelRequest;
                userIDList.Remove(LoggedInUserUserId);
                var result = await _noteService.UpdateNoteText(LoggedInUserUserId, noteDtoText);
                await Clients.Users(userIDList).SendAsync("update-note-text", noteDtoText);
            }
        }

        [HubMethodName("share-note-with-user")]
        public async Task<ServiceResponse> ShareNoteWithUser(AddSharedUserRequest request)
        {
            var result = await _noteService.CanAddSharedUser(LoggedInUserUserId, request.Email, request.NoteTextID);
            if(!result.Success)
            {
                return result;
            }
            
            var resultNotificationDto = await _notificationService.SendReqestSharedNotification(LoggedInUserUserId, request.Email, request.NoteTextID);
            if(!resultNotificationDto.Success)
            {
                return resultNotificationDto.convertToServiceRespose();
            }
            var sharedUser = await _repositoryUsers.GetByAsync(el => el.Email == request.Email);

            await _hubContext.Clients.User(sharedUser.Id).SendAsync("send-new-notification", resultNotificationDto.ModelRequest);

            return new ServiceResponse();
        }

        [HubMethodName("accept-shared-note")]
        public async Task<NoteDto> AcceptSharedNote(int noteTextID, int notificationID)
        {
            var result = await _noteService.AcceptSharedNote(LoggedInUserUserId, noteTextID, notificationID);
            var ownerNoteIDResult = await _noteService.GetOwnerID(LoggedInUserUserId, noteTextID);

            if(!ownerNoteIDResult.Success)
            {
                return null;
            }

            var ownerNoteID = ownerNoteIDResult.ModelRequest;

            var resultNotificationDto = await _notificationService.SendAcceptReqestSharedNotification(LoggedInUserUserId, ownerNoteID, noteTextID);

            await _hubContext.Clients.User(ownerNoteID).SendAsync("send-new-notification", resultNotificationDto.ModelRequest);
            return result.ModelRequest;
        }

        [HubMethodName("decline-shared-note")]
        public async Task DeclineSharedNote(int noteTextID, int notificationID)
        {
            var result = await _noteService.DeclineSharedNote(LoggedInUserUserId, noteTextID, notificationID);
            var ownerNoteIDResult = await _noteService.GetOwnerID(LoggedInUserUserId, noteTextID);

            if (!ownerNoteIDResult.Success)
            {
                return;
            }

            var ownerNoteID = ownerNoteIDResult.ModelRequest;

            var resultNotificationDto = await _notificationService.SendDeclineReqestSharedNotification(LoggedInUserUserId, ownerNoteID, noteTextID);

            await _hubContext.Clients.User(ownerNoteID).SendAsync("send-new-notification", resultNotificationDto.ModelRequest);
        }


        private string LoggedInUserUserId
        {
            get
            {
                var currentUser = Context.User;
                var currentUserID = currentUser.FindFirst(i => i.Type == JwtClaimTypes.Subject).Value;
                return currentUserID;
            }
        }
    }
}
