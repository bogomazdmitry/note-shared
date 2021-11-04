using AutoMapper;
using NoteShared.DTO.DTO;
using NoteShared.Infrastructure.Data.Entities.Notifications;

namespace NoteShared.DTO.Mapping
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDto, Notification>()
                .ForMember(notification => notification.ID, map => map.MapFrom(notificationDto => notificationDto.ID))
                .ForMember(notification => notification.Content, map => map.MapFrom(notificationDto => notificationDto.Content))
                .ForMember(notification => notification.Type, map => map.MapFrom(notificationDto => notificationDto.Type));

            CreateMap<Notification, NotificationDto>()
                .ForMember(notificationDto => notificationDto.ID, map => map.MapFrom(notification => notification.ID))
                .ForMember(notificationDto => notificationDto.Content, map => map.MapFrom(notification => notification.Content))
                .ForMember(notificationDto => notificationDto.Type, map => map.MapFrom(notification => notification.Type));

            CreateMap<NotificationType, NotificationTypeDto>().ReverseMap();
        }
    }
}
