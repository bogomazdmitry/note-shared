using AutoMapper;
using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;
using NoteShared.Infrastructure.Data.Entity.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public class NoteService : INoteService
    {
        private readonly IRepositoryNotes _repositoryNotes;

        private readonly IRepositoryNoteDesigns _repositoryNotesDesigns;

        private readonly IRepositoryNoteTexts _repositoryNoteTexts;

        private readonly IRepositioryUsers _repositoryUsers;

        private readonly IMapper _mapper;

        public NoteService(
            IRepositoryNotes repositoryNotes,
            IRepositoryNoteDesigns repositoryNoteDesigns,
            IRepositoryNoteTexts repositoryNoteTexts,
            IRepositioryUsers repositoryUsers,
            IMapper mapper
        )
        {
            _repositoryNotes = repositoryNotes;
            _repositoryNotesDesigns = repositoryNoteDesigns;
            _repositoryNoteTexts = repositoryNoteTexts;
            _repositoryUsers = repositoryUsers;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<NoteDto>>> GetAllNotes(string userID)
        {
            var notes = _repositoryNotes
                .GetAllByQueryable(e => e.UserID == userID, e => e.NoteDesign, e => e.NoteText)
                .OrderBy(e => e.Order)
                .ToList();
            var noteDtoList = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteDto>>(notes);
            return new ServiceResponse<IEnumerable<NoteDto>>(noteDtoList);
        }

        public async Task<ServiceResponse<NoteDto>> GetNote(string userID, int noteID)
        {
            if (!(await _repositoryNotes.HasAccessForUser(noteID, userID)))
            {
                return new ServiceResponse<NoteDto>("Not allowed");
            }

            var note = await _repositoryNotes.GetByAsync(e => e.UserID == userID && e.ID == noteID);
            var noteDto = _mapper.Map<Note, NoteDto>(note);
            return new ServiceResponse<NoteDto>(noteDto);
        }

        public async Task<ServiceResponse<NoteDto>> CreateNote(string userID, NoteDto noteDto)
        {
            var newNote = _mapper.Map<NoteDto, Note>(noteDto);
            newNote.UserID = userID;
            var createdNote = await _repositoryNotes.CreateAsync(newNote);
            var createdNoteDto = _mapper.Map<NoteDto>(createdNote);
            return new ServiceResponse<NoteDto>(createdNoteDto);
        }

        public async Task<ServiceResponse> DeleteNote(string userID, int deleteNoteID)
        {
            if (!(await _repositoryNotes.HasAccessForUser(deleteNoteID, userID)))
            {
                return new ServiceResponse("Not allowed");
            }
            var deleteNote = await _repositoryNotes.GetByAsync(note => note.ID == deleteNoteID);
            await _repositoryNotes.RemoveAsync(deleteNote);
            return new ServiceResponse();

        }

        public async Task<ServiceResponse<NoteTextDto>> UpdateNoteText(string userID, NoteTextDto updateNoteTextDto)
        {
            if (!_repositoryNoteTexts.HasAccessForUser(updateNoteTextDto.ID, userID))
            {
                return new ServiceResponse<NoteTextDto>("Not allowed");
            }

            var oldNoteText = await _repositoryNoteTexts.GetByAsync(el => el.ID == updateNoteTextDto.ID);
            if (oldNoteText == null)
            {
                oldNoteText = await _repositoryNoteTexts.CreateAsync(_mapper.Map<NoteTextDto, NoteText>(updateNoteTextDto));
            }
            else
            {
                _mapper.Map(updateNoteTextDto, oldNoteText);
                oldNoteText = await _repositoryNoteTexts.UpdateAsync(oldNoteText);
            }

            return new ServiceResponse<NoteTextDto>(_mapper.Map<NoteText, NoteTextDto>(oldNoteText));
        }

        public async Task<ServiceResponse<NoteDto>> UpdateNote(string userID, NoteDto updateNoteDto)
        {
            if (!(await _repositoryNotes.HasAccessForUser(updateNoteDto.ID, userID)))
            {
                return new ServiceResponse<NoteDto>("Not allowed");
            }

            var oldNote = await _repositoryNotes.GetByAsync(el => el.ID == updateNoteDto.ID);
            _mapper.Map(updateNoteDto, oldNote);
            var updatedNote = await _repositoryNotes.UpdateAsync(oldNote);

            var updatedNoteDto = _mapper.Map<Note, NoteDto>(updatedNote);
            return new ServiceResponse<NoteDto>(updatedNoteDto);
        }

        public async Task<ServiceResponse<NoteDesignDto>> UpdateNoteDesign(string userID, NoteDesignDto updateNoteDesignDto)
        {
            if (!(await _repositoryNotes.HasAccessForUser(updateNoteDesignDto.NoteID, userID)))
            {
                return new ServiceResponse<NoteDesignDto>("Not allowed");
            }

            var oldNoteDesign = await _repositoryNotesDesigns.GetByAsync(el => el.NoteID == updateNoteDesignDto.NoteID);
            NoteDesign updatedNoteDesign;
            if (oldNoteDesign == null)
            {
                updatedNoteDesign = await _repositoryNotesDesigns.CreateAsync(_mapper.Map<NoteDesignDto, NoteDesign>(updateNoteDesignDto));
            }
            else
            {
                _mapper.Map(updateNoteDesignDto, oldNoteDesign);
                updatedNoteDesign = await _repositoryNotesDesigns.UpdateAsync(oldNoteDesign);
            }

            var updatedNoteDesignDto = _mapper.Map<NoteDesign, NoteDesignDto>(updatedNoteDesign);
            return new ServiceResponse<NoteDesignDto>(updatedNoteDesignDto);
        }

        public async Task<ServiceResponse<IEnumerable<NoteOrderDto>>> UpdateOrderNotes(string userID, IEnumerable<NoteOrderDto> noteOrderDtoList)
        {
            var noteList = _repositoryNotes.GetAllByQueryable(note => note.UserID == userID).ToList();
            noteList.AsParallel().ForAll(note =>
            {
                note.Order = noteOrderDtoList.FirstOrDefault(noteOrder => noteOrder.ID == note.ID)?.Order ?? -1;
            });

            await _repositoryNotes.UpdateRangeAsync(noteList);
            return new ServiceResponse<IEnumerable<NoteOrderDto>>(noteOrderDtoList);
        }

        public async Task<ServiceResponse<List<string>>> GetUserIDListByNoteTextID(string userID, int noteTextID)
        {
            var note = await _repositoryNotes.GetByAsync(note => note.UserID == userID && note.NoteTextID == noteTextID);
            if (note == null)
            {
                return new ServiceResponse<List<string>>("Not allowed");
            }

            var userList = _repositoryNoteTexts.GetUserIDList(noteTextID);
            return new ServiceResponse<List<string>>(userList);
        }

        public async Task<ServiceResponse<List<string>>> GetUserEmailListByNoteTextID(string userID, int noteTextID)
        {
            var note = await _repositoryNotes.GetByAsync(el => el.UserID == userID && el.NoteTextID == noteTextID);
            if (note == null)
            {
                return new ServiceResponse<List<string>>("Not allowed");
            }

            var userList = _repositoryNoteTexts.GetUserEmailList(noteTextID);
            return new ServiceResponse<List<string>>(userList);
        }

        public async Task<ServiceResponse> AddSharedUser(string currentUserID, string sharedUserEmail, int noteTextID)
        {
            if (!_repositoryNoteTexts.HasAccessForUser(noteTextID, currentUserID))
            {
                return new ServiceResponse("Not allowed");
            }

            var noteText = await _repositoryNoteTexts.GetByAsync(el => el.ID == noteTextID);

            var sharedUser = await _repositoryUsers.GetByAsync(el => el.Email == sharedUserEmail);
            if (sharedUser == null)
            {
                return new ServiceResponse("User is not found");
            }

            int order = 0;
            if (_repositoryNotes.HasNote(sharedUser.Id))
            {
                order = _repositoryNotes.GetMinimalNoteOrder(sharedUser.Id) - 1;
            }

            Note newNote = new Note { Order = order, UserID = sharedUser.Id, NoteTextID = noteTextID };
            await _repositoryNotes.CreateAsync(newNote);

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> DeleteSharedUser(string currentUserID, string sharedUserEmail, int noteTextID)
        {
            if (!_repositoryNoteTexts.HasAccessForUser(noteTextID, currentUserID))
            {
                return new ServiceResponse("Not allowed");
            }

            var sharedUser = await _repositoryUsers.GetByAsync(user => user.Email == sharedUserEmail);
            if (sharedUser == null)
            {
                return new ServiceResponse();
            }

            var deleteNote = await _repositoryNotes.GetByAsync(note => note.NoteTextID == noteTextID && note.UserID == sharedUser.Id);
            await _repositoryNotes.RemoveAsync(deleteNote);

            return new ServiceResponse();
        }
    }
}
