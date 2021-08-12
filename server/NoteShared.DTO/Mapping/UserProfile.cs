using AutoMapper;
using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.DTO.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {
            CreateMap<User, UserInfoResponse>();
        }
    }
}
