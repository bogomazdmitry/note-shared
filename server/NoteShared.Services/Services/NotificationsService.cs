using AutoMapper;
using Newtonsoft.Json;
using NoteShared.DTO;
using NoteShared.DTO.DTO;
using NoteShared.Infrastructure.Data.Entities.Notifications;
using NoteShared.Infrastructure.Data.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public class NotificationsService : INotificationsService
    {
        private readonly IRepositoryNotifications _repositoryNotifications;

        private readonly IMapper _mapper;

        private readonly IRepositioryUsers _repositoryUsers;

        public NotificationsService(IRepositoryNotifications repositoryNotifications, IMapper mapper, IRepositioryUsers repositoryUsers)
        {
            _repositoryNotifications = repositoryNotifications;
            _mapper = mapper;
            _repositoryUsers = repositoryUsers;
        }

        public async Task<ServiceResponse<IEnumerable<NotificationDto>>> GetNotifications(string userID)
        {
            var notificationList = _repositoryNotifications
                .GetAllByQueryable(e => e.UserID == userID)
                .OrderByDescending(e => e.CreateDateTime)
                .ToList();
            var notificationDtoList = _mapper.Map<IEnumerable<NotificationDto>>(notificationList);
            return new ServiceResponse<IEnumerable<NotificationDto>>(notificationDtoList);
        }

        public async Task<ServiceResponse> DeleteNotification(string userID, int deleteNotificationID)
        {
            var deleteNote = await _repositoryNotifications.GetByAsync(el => el.ID == deleteNotificationID);
            if (deleteNote.UserID != userID)
            {
                return new ServiceResponse("Not allowed");
            }
            await _repositoryNotifications.RemoveAsync(deleteNote);
            return new ServiceResponse();
        }

        public async Task<ServiceResponse<NotificationDto>> SendReqestSharedNotification(string currentUserID, string sharedUserEmail, int noteTextID)
        {
            var sharedUser = await _repositoryUsers.GetByAsync(el => el.Email == sharedUserEmail);
            var currentUser = await _repositoryUsers.GetByAsync(el => el.Id == currentUserID);

            var notificationContent = new RequestSharedNoteNotificationContent()
            {
                FromUserEmail = currentUser.Email,
                NoteTextID = noteTextID
            };

            var notification = new Notification()
            {
                UserID = sharedUser.Id,
                Content = JsonConvert.SerializeObject(notificationContent),
                Type = NotificationType.RequestSharedNoteType,
                CreateDateTime = DateTime.UtcNow
            };

            await _repositoryNotifications.CreateAsync(notification);

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            return new ServiceResponse<NotificationDto>(notificationDto);
        }

        public async Task<ServiceResponse<NotificationDto>> SendAcceptReqestSharedNotification(string currentUserID, string ownerUserID, int noteTextID)
        {
            var currentUser = await _repositoryUsers.GetByAsync(el => el.Id == currentUserID);

            var notificationContent = new AcceptedRequestSharedNoteNotificationContent()
            {
                FromUserEmail = currentUser.Email,
                NoteText = noteTextID,
            };

            var notification = new Notification()
            {
                UserID = ownerUserID,
                Content = JsonConvert.SerializeObject(notificationContent),
                Type = NotificationType.AcceptedRequestSharedNoteType,
                CreateDateTime = DateTime.UtcNow
            };

            await _repositoryNotifications.CreateAsync(notification);

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            return new ServiceResponse<NotificationDto>(notificationDto);
        }

        public async Task<ServiceResponse<NotificationDto>> SendDeclineReqestSharedNotification(string currentUserID, string ownerUserID, int noteTextID)
        {
            var currentUser = await _repositoryUsers.GetByAsync(el => el.Id == currentUserID);

            var notificationContent = new DeclinedRequestSharedNoteNotificationContent()
            {
                FromUserEmail = currentUser.Email,
                NoteTextID = noteTextID
            };

            var notification = new Notification()
            {
                UserID = ownerUserID,
                Content = JsonConvert.SerializeObject(notificationContent),
                Type = NotificationType.DeclinedRequestSharedNoteType,
                CreateDateTime = DateTime.UtcNow
            };

            await _repositoryNotifications.CreateAsync(notification);

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            return new ServiceResponse<NotificationDto>(notificationDto);
        }
    }
}
