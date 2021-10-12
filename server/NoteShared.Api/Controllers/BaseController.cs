using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using NoteShared.DTO;

namespace NoteShared.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public string? LoggedInUserUserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return User.FindFirst(i => i.Type == JwtClaimTypes.Subject).Value;
                }

                return null;
            }
        }

        protected IActionResult ResultOf<TModel>(ServiceResponse<TModel> answer)
        {
            if (answer.Success)
            {
                return Ok(answer.ModelRequest);
            }
            return BadRequest(answer.Error);
        }

        protected IActionResult ResultOf(ServiceResponse answer)
        {
            if (answer.Success)
            {
                return Ok();
            }
            return BadRequest(answer.Error);
        }
    }
}
