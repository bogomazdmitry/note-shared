using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using NoteShared.DTO;

namespace NoteShared.Api.Controllers
{
    public class BaseController : ControllerBase
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? LoggedInUserUserId
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
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
