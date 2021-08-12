using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteShared.DTO;
using NoteShared.Services.Interfaces;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace NoteShared.Api.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(LocalApi.PolicyName)]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetUserInfo(LoggedInUserUserId);
            return ResultOf(result);
        }

        [HttpPost]
        [Authorize(LocalApi.PolicyName)]
        public async Task<IActionResult> Post([FromBody] ChangeUserInfoRequest userInfo)
        {
            var result = await _userService.SetUserInfo(userInfo, LoggedInUserUserId);
            return ResultOf(result);
        }
    }
}
