using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.Notes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public class NoteService
    {
        private readonly IRepositoryNotes _repositoryNotes;

        public NoteService(IRepositoryNotes repositoryNotes)
        {
            _repositoryNotes = repositoryNotes;
        }

        public async Task<ServiceRespose<IEnumerable<Note>>> GetAllNotes(string userID)
        {
            return new ServiceRespose<IEnumerable<Note>>(_repositoryNotes.GetAllByQueryable(e => e.UserID == userID).AsEnumerable());
        }

        public async Task<ServiceRespose<Note>> GetNote(string userID, int noteId)
        {
            var note = await _repositoryNotes.GetByAsync(e => e.UserID == userID);

            var access = await HasAccess(note, userID);
            if (access.Success)
            {
                return new ServiceRespose<Note>(note);
            }
            else
            {
                return new ServiceRespose<Note>(access);
            }
        }

        public async Task<ServiceRespose<Note>> CreateNote(Note note)
        {
            return new ServiceRespose<Note>(await _repositoryNotes.CreateAsync(note));
        }

        public async Task<ServiceRespose> DeleteNote(string userID, int id)
        {
            var note = await _repositoryNotes.GetByAsync(el => el.ID == id);

            var access = await HasAccess(note, userID);
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

        public async Task<ServiceRespose<Note>> UpdateNote(string userID, Note note)
        {
            var dbNote = await _repositoryNotes.GetByAsync(el => el.ID == note.ID);

            var access = await HasAccess(note, userID);
            if (!access.Success)
            {
                return new ServiceRespose<Note>(access);
            }
            access = await HasAccess(note, dbNote.UserID);
            if (!access.Success)
            {
                return new ServiceRespose<Note>("Change id is not allowed");
            }
            ///
            dbNote.Design = note.Design;
            dbNote.DesignID = note.DesignID;
            dbNote.HistoryID = note.HistoryID;
            dbNote.Order = note.Order;
            dbNote.NoteHistory = note.NoteHistory;
            dbNote.Text = note.Text;
            dbNote.Tittle = note.Tittle;
            var updatedNote = await _repositoryNotes.UpdateAsync(dbNote);
            return new ServiceRespose<Note>(updatedNote);
        }

        public async Task<ServiceRespose<IEnumerable<Note>>> UpdateOrderNotes(string userID, IEnumerable<NoteOrder> notesOrder)
        {
            var noteList = _repositoryNotes.GetAllByQueryable(note => note.UserID == userID).ToList();
            noteList.AsParallel().ForAll(note => {
                note.Order = notesOrder.FirstOrDefault(noteOrder => noteOrder.ID == note.ID).Order;
            });
            return new ServiceRespose<IEnumerable<Note>>();
        }

        public async Task<ServiceRespose> HasAccess(Note note, string userID)
        {
            if (note.UserID == userID)
            {
                return new ServiceRespose();
            }
            else
            {
                return new ServiceRespose("Not allowed");
            }
        }
    }
}
