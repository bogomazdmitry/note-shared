using NoteShared.DTO;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserInfoResponse>> GetUserInfo(string userId);

        Task<ServiceResponse<UserInfoResponse>> SetUserInfo(ChangeUserInfoRequest userInfo, string userId);
    }
}
