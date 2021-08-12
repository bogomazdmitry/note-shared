using AutoMapper;
using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;

namespace NoteShared.DTO.Mapping
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            AllowNullCollections = true;
            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();

            CreateMap<NoteText, NoteTextDto>();
            CreateMap<NoteTextDto, NoteText>();


            CreateMap<NoteHistory, NoteHistoryDto>();
            CreateMap<NoteHistoryDto, NoteHistory>();

            CreateMap<NoteDesign, NoteDesignDto>();
            CreateMap<NoteDesignDto, NoteDesign>();
        }
    }
}
