using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NoteShared.Services.Interfaces;
using NoteShared.DTO;

namespace NoteShared.Api.Controllers
{
    [Route("api/auth/")]
    [ApiController] 
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserRequest signUnUser)
        {
            var result = await _authService.SignUp(signUnUser);
            return ResultOf(result);
        }

        [HttpGet]
        [Route("check-unique-email")]
        public async Task<IActionResult> CheckUniqueEmail([FromQuery] string email)
        {
            var result = await _authService.CheckUniqueEmail(email);
            return ResultOf(result);
        }

        [HttpGet]
        [Route("check-unique-user-name")]
        public async Task<IActionResult> CheckUniqueUserName([FromQuery] string userName)
        {
            var result = await _authService.CheckUniqueUserName(userName);
            return ResultOf(result);
        }
    }
}
