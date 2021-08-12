using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.Users;
using System;
using NoteShared.DTO.Mapping;
using AutoMapper;

namespace NoteShared.Services.Interfaces
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ServiceRespose<UserInfoResponse>> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            UserInfoResponse userInfo = _mapper.Map<User, UserInfoResponse>(user);
            return new ServiceRespose<UserInfoResponse>(userInfo);
        }

        public async Task<ServiceRespose<UserInfoResponse>> SetUserInfo(ChangeUserInfoRequest userInfo, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (!String.IsNullOrEmpty(userInfo.NewPassword))
            {
                var identityChangePasswordResult = await _userManager.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.NewPassword);
                if (!identityChangePasswordResult.Succeeded)
                {
                    return new ServiceRespose<UserInfoResponse>("oldPassword mastMatch");
                }
            }
            return new ServiceRespose<UserInfoResponse>(_mapper.Map<User, UserInfoResponse>(user));
        }
    }
}
