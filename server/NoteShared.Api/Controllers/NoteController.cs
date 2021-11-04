using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteShared.Api.Controllers;
using NoteShared.DTO;
using NoteShared.DTO.Request;
using NoteShared.Services.Interfaces;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Api.Controllers
{
    [Route("api/note")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    public class NoteController : BaseController
    {
        private readonly ILogger<NoteController> _logger;

        private readonly INoteService _noteService;

        public NoteController(ILogger<NoteController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int noteID)
        {
            var result = await _noteService.GetNote(LoggedInUserUserId, noteID);
            return ResultOf(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] int noteOrder)
        {
            var note = new NoteDto { Order = noteOrder, NoteText = new NoteTextDto() };
            var result = await _noteService.CreateNote(LoggedInUserUserId, note);
            return ResultOf(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int noteID)
        {
            var result = await _noteService.DeleteNote(LoggedInUserUserId, noteID);
            return ResultOf(result);
        }

        [Route("update-note")]
        [HttpPost]
        public async Task<IActionResult> UpdateNote([FromBody] NoteDto updateNoteDto)
        {
            var result = await _noteService.UpdateNote(LoggedInUserUserId, updateNoteDto);
            return ResultOf(result);
        }

        [Route("update-note-design")]
        [HttpPost]
        public async Task<IActionResult> UpdateDesignNote([FromBody] NoteDesignDto updateNoteDesignDto)
        {
            var result = await _noteService.UpdateNoteDesign(LoggedInUserUserId, updateNoteDesignDto);
            return ResultOf(result);
        }

        [Route("shared-users-emails")]
        [HttpGet]
        public async Task<IActionResult> GetSharedUserEmails([FromQuery] int noteTextID)
        {
            var result = await _noteService.GetUserEmailListByNoteTextID(LoggedInUserUserId, noteTextID);
            return ResultOf(result);
        }

        [Route("delete-shared-user")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSharedUser([FromQuery] DeleteSharedUserRequest deleteSharedUserRequest)
        {
            var result = await _noteService.DeleteSharedUser(LoggedInUserUserId, deleteSharedUserRequest.Email, deleteSharedUserRequest.NoteTextID);
            return ResultOf(result);
        }
    }
}
