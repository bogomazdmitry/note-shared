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
    public class NoteService
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
        ) {
            _repositoryNotes = repositoryNotes;
            _repositoryNotesDesigns = repositoryNoteDesigns;
            _repositoryNoteTexts = repositoryNoteTexts;
            _repositoryUsers = repositoryUsers;
            _mapper = mapper;
        }

        public async Task<ServiceRespose<IEnumerable<NoteDto>>> GetAllNotes(string userID)
        {
            var notes = _repositoryNotes.GetAllByQueryable(e => e.UserID == userID, e => e.NoteDesign, e => e.NoteText).OrderBy(e => e.Order).ToList();
            var noteDtos = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteDto>>(notes);
            return new ServiceRespose<IEnumerable<NoteDto>>(noteDtos);
        }

        public async Task<ServiceRespose<NoteDto>> GetNote(string userID, int noteId)
        {
            var note = await _repositoryNotes.GetByAsync(e => e.UserID == userID);

            var access = await HasAccess(note.ID, userID);
            if (access.Success)
            {
                var noteDto = _mapper.Map<Note, NoteDto>(note);
                return new ServiceRespose<NoteDto>(noteDto);
            }
            else
            {
                return new ServiceRespose<NoteDto>(access);
            }
        }

        public async Task<ServiceRespose<NoteDto>> CreateNote(string userID, NoteDto noteDto)
        {
            var note = _mapper.Map<NoteDto, Note>(noteDto);
            note.UserID = userID;
            return new ServiceRespose<NoteDto>(_mapper.Map<NoteDto>(await _repositoryNotes.CreateAsync(note)));
        }

        public async Task<ServiceRespose> DeleteNote(string userID, int id)
        {
            var note = await _repositoryNotes.GetByAsync(el => el.ID == id);

            var access = await HasAccess(note.ID, userID);
            if (access.Success)
            {
                await _repositoryNotes.RemoveAsync(note);
                return new ServiceRespose();
            }
            else
            {
                return access;
            }
        }

        public async Task<ServiceRespose<NoteTextDto>> UpdateNoteText(string userID, NoteTextDto noteTextDto)
        {
            var access = _repositoryNoteTexts.GetUserIDsAsQueryable(noteTextDto.ID).Contains(userID);

            if (!access)
            {
                return new ServiceRespose<NoteTextDto>("Not allowed");
            }

            var noteTextDb = await _repositoryNoteTexts.GetByAsync(el => el.ID == noteTextDto.ID);
            if (noteTextDb == null)
            {
                noteTextDb = await _repositoryNoteTexts.CreateAsync(_mapper.Map<NoteTextDto, NoteText>(noteTextDto));
            }
            else
            {
                _mapper.Map(noteTextDto, noteTextDb);
                noteTextDb = await _repositoryNoteTexts.UpdateAsync(noteTextDb);
            }

            return new ServiceRespose<NoteTextDto>(_mapper.Map<NoteText, NoteTextDto>(noteTextDb));
        }

        public async Task<ServiceRespose<NoteDto>> UpdateNote(string userID, NoteDto noteDto)
        {
            var dbNote = await _repositoryNotes.GetByAsync(el => el.ID == noteDto.ID);
            var access = await HasAccess(dbNote.ID, userID);
            if (!access.Success)
            {
                return new ServiceRespose<NoteDto>(access);
            }

            _mapper.Map(noteDto, dbNote);
            var updatedNote = await _repositoryNotes.UpdateAsync(dbNote);
            
            return new ServiceRespose<NoteDto>(_mapper.Map<Note, NoteDto>(updatedNote));
        }

        public async Task<ServiceRespose<NoteDesignDto>> UpdateNoteDesign(string userID, NoteDesignDto noteDesignDto)
        {
            var dbNote = await _repositoryNotes.GetByAsync(el => el.ID == noteDesignDto.NoteID);

            var access = await HasAccess(dbNote.ID, userID);
            if (!access.Success)
            {
                return new ServiceRespose<NoteDesignDto>(access);
            }

            var noteDesignDb = await _repositoryNotesDesigns.GetByAsync(el=>el.NoteID == noteDesignDto.NoteID);
            if (noteDesignDb == null)
            {
                noteDesignDb = await _repositoryNotesDesigns.CreateAsync(_mapper.Map<NoteDesignDto, NoteDesign>(noteDesignDto));
            }
            else
            {
                _mapper.Map(noteDesignDto, noteDesignDb);
                noteDesignDb = await _repositoryNotesDesigns.UpdateAsync(noteDesignDb);
            }

            return new ServiceRespose<NoteDesignDto>(_mapper.Map<NoteDesign, NoteDesignDto>(noteDesignDb));
        }

        public async Task<ServiceRespose<IEnumerable<NoteOrderDto>>> UpdateOrderNotes(string userID, IEnumerable<NoteOrderDto> notesOrderDto)
        {
            var noteList = _repositoryNotes.GetAllByQueryable(note => note.UserID == userID).ToList();
            noteList.AsParallel().ForAll(note => {
                note.Order = notesOrderDto.FirstOrDefault(noteOrder => noteOrder.ID == note.ID).Order;
            });

            await _repositoryNotes.UpdateRangeAsync(noteList);
            return new ServiceRespose<IEnumerable<NoteOrderDto>>(notesOrderDto);
        }

        public async Task<ServiceRespose> HasAccess(int noteID, string userID)
        {
            if (_repositoryNotes.GetUsersID(noteID).Contains(userID))
            {
                return new ServiceRespose();
            }
            else
            {
                return new ServiceRespose("Not allowed");
            }
        }

        public async Task<ServiceRespose<List<string>>> GetUserIDsByNoteTextID(string userID, int noteTextID)
        {
            var note = _repositoryNotes.GetByAsync(el => el.UserID == userID && el.NoteTextID == noteTextID);
            if (note == null)
            {
                return new ServiceRespose<List<string>>("Not allowed");
            }
            return new ServiceRespose<List<string>>(_repositoryNoteTexts.GetUserIDsAsQueryable(noteTextID).ToList());
        }

        public async Task<ServiceRespose<List<string>>> GetUserEmailsByNoteTextID(string userID, int noteTextID)
        {
            var note = _repositoryNotes.GetByAsync(el => el.UserID == userID && el.NoteTextID == noteTextID);
            if (note == null)
            {
                return new ServiceRespose<List<string>>("Not allowed");
            }
            return new ServiceRespose<List<string>>(_repositoryNoteTexts.GetUserEmailsAsQueryable(noteTextID).ToList());
        }
        
        public async Task<ServiceRespose> AddSharedUser(string currentUserID, string sharedUserEmail, int noteTextID)
        {
            var access = _repositoryNoteTexts.GetUserIDsAsQueryable(noteTextID).Contains(currentUserID);
            if (!access)
            {
                return new ServiceRespose("Not allowed");
            }

            var noteText = await _repositoryNoteTexts.GetByAsync(el => el.ID == noteTextID);

            var sharedUser = await _repositoryUsers.GetByAsync(el => el.Email == sharedUserEmail);
            if(sharedUser == null)
            {
                return new ServiceRespose("User is not found");
            }

            var request = _repositoryNotes.GetAllByQueryable(el => el.UserID == sharedUser.Id).Select(el => el.Order);
            
            int order = 0;
            if (request.Any())
            {
                order = request.Min() - 1;
            }
            
            Note note = new Note { Order = order, UserID = sharedUser.Id, NoteTextID = noteTextID };
            await _repositoryNotes.CreateAsync(note);

            return new ServiceRespose();
        }

        public async Task<ServiceRespose> DeleteSharedUser(string currentUserID, string sharedUserEmail, int noteTextID)
        {
            var access = _repositoryNoteTexts.GetUserIDsAsQueryable(noteTextID).Contains(currentUserID);
            if (!access)
            {
                return new ServiceRespose("Not allowed");
            }

            var sharedUser = await _repositoryUsers.GetByAsync(el => el.Email == sharedUserEmail);
            if (sharedUser == null)
            {
                return new ServiceRespose();
            }

            var note = await _repositoryNotes.GetByAsync(el => el.NoteTextID == noteTextID && el.UserID == sharedUser.Id);
            await _repositoryNotes.RemoveAsync(note);

            return new ServiceRespose();
        }
    }
}
