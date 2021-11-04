using NoteShared.DTO;
using NoteShared.DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public interface INotificationsService
    {
        Task<ServiceResponse<IEnumerable<NotificationDto>>> GetNotifications(string userID);

        Task<ServiceResponse> DeleteNotification(string userID, int deleteNotificationID);

        Task<ServiceResponse<NotificationDto>> SendReqestSharedNotification(string currentUserID, string sharedUserEmail, int noteTextID);

        Task<ServiceResponse<NotificationDto>> SendAcceptReqestSharedNotification(string currentUserID, string sharedUserID, int noteTextID);

        Task<ServiceResponse<NotificationDto>> SendDeclineReqestSharedNotification(string currentUserID, string sharedUserEmail, int noteTextID);
    }
}
