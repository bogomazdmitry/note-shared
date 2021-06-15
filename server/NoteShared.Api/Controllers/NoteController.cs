using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteShared.Api.Controllers;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Services.Interfaces;
using System.Collections.Generic;
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
        private readonly NoteService _noteService;

        public NoteController(ILogger<NoteController> logger, NoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _noteService.GetNote(LoggedInUserUserId, id);
            return ResultOf(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody]Note note)
        //{
        //    var result = await _noteService.CreateNote(note);
        //    return ResultOf(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] int order)
        {
            Note note = new Note { Order = order, Tittle = "", Text="", Design = null, NoteHistory = null, UserID = LoggedInUserUserId};
            var result = await _noteService.CreateNote(note);
            return ResultOf(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _noteService.DeleteNote(LoggedInUserUserId, id);
            return ResultOf(result);
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Note note)
        {
            var result = await _noteService.UpdateNote(LoggedInUserUserId, note);
            return ResultOf(result);
        }
    }
}
