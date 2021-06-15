using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.Users;
using System;

namespace NoteShared.Services.Interfaces
{
    public class UserService
    {
        private readonly IRepositioryUser _repositioryUser;
        private readonly UserManager<User> _userManager;
        private readonly AuthService _authService;

        public UserService(UserManager<User> userManager, IRepositioryUser repositioryUser, AuthService authService)
        {
            _repositioryUser = repositioryUser;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<ServiceRespose<UserInfoResponse>> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            UserInfoResponse userInfo = new UserInfoResponse { Email = user.Email, UserName = user.UserName };
            return new ServiceRespose<UserInfoResponse>(userInfo);
        }

        public async Task<ServiceRespose<UserInfoResponse>> SetUserInfo(ChangeUserInfoRequest userInfo, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (!String.IsNullOrEmpty(userInfo.NewPassword))
            {
                var identityChangePasswordResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, userInfo.NewPassword);
                if (!identityChangePasswordResult.Succeeded)
                {
                    return new ServiceRespose<UserInfoResponse>("oldPassword mastMatch");
                }
            }
            if (user.Email != userInfo.Email)
            {
                var emalUnique = await _authService.CheckUniqueEmail(userInfo.Email);
                if (!emalUnique.Success)
                {
                    return new ServiceRespose<UserInfoResponse> (emalUnique);
                }
            }
            if (user.UserName != userInfo.UserName)
            {
                var userNameUnique = await _authService.CheckUniqueUserName(userInfo.UserName);
                if (!userNameUnique.Success)
                {
                    return new ServiceRespose<UserInfoResponse>(userNameUnique);
                }
            }
            user.Email = userInfo.Email;
            user.UserName = userInfo.UserName;
            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                return new ServiceRespose<UserInfoResponse>(identityResult.Errors.ToList().ToString());
            }
            return new ServiceRespose<UserInfoResponse>();
        }
    }
}
