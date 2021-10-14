using AutoMapper;
using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.DTO.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfoResponse>()
                .ForMember(user => user.Email, map => map.MapFrom(userInfoResponse => userInfoResponse.Email))
                .ForMember(user => user.UserName, map => map.MapFrom(userInfoResponse => userInfoResponse.UserName));
        }
    }
}
