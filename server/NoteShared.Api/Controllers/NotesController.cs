using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteShared.Api.Controllers;
using NoteShared.DTO;
using NoteShared.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Api.Controllers
{
    [Route("api/notes")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    public class NotesController : BaseController
    {
        private readonly ILogger<NotesController> _logger;

        private readonly INoteService _noteService;

        public NotesController(ILogger<NotesController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _noteService.GetAllNotes(LoggedInUserUserId);
            return ResultOf(result);
        }

        [Route("update-order")]
        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] IEnumerable<NoteOrderDto> notesOrder)
        {
            var result = await _noteService.UpdateOrderNotes(LoggedInUserUserId, notesOrder);
            return ResultOf(result);
        }
    }
}
