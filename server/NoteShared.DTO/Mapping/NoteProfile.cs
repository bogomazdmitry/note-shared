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

            CreateMap<Note, NoteDto>()
                .ForMember(noteDto => noteDto.ID, map => map.MapFrom(note => note.ID))
                .ForMember(noteDto => noteDto.NoteDesign, map => map.MapFrom(note => note.NoteDesign))
                .ForMember(noteDto => noteDto.NoteHistory, map => map.MapFrom(note => note.NoteHistory))
                .ForMember(noteDto => noteDto.NoteText, map => map.MapFrom(note => note.NoteText))
                .ForMember(noteDto => noteDto.Order, map => map.MapFrom(note => note.Order));

            CreateMap<NoteDto, Note>()
                .ForMember(note => note.ID, map => map.MapFrom(noteDto => noteDto.ID))
                .ForMember(note => note.NoteDesign, map => map.MapFrom(noteDto => noteDto.NoteDesign))
                .ForMember(note => note.NoteHistory, map => map.MapFrom(noteDto => noteDto.NoteHistory))
                .ForMember(note => note.NoteText, map => map.MapFrom(noteDto => noteDto.NoteText))
                .ForMember(note => note.Order, map => map.MapFrom(noteDto => noteDto.Order));

            CreateMap<NoteText, NoteTextDto>()
                .ForMember(noteTextDto => noteTextDto.ID, map => map.MapFrom(noteText => noteText.ID))
                .ForMember(noteTextDto => noteTextDto.Text, map => map.MapFrom(noteText => noteText.Text))
                .ForMember(noteTextDto => noteTextDto.Title, map => map.MapFrom(noteText => noteText.Title));

            CreateMap<NoteTextDto, NoteText>()
                .ForMember(noteText => noteText.ID, map => map.MapFrom(noteTextDto => noteTextDto.ID))
                .ForMember(noteText => noteText.Text, map => map.MapFrom(noteTextDto => noteTextDto.Text))
                .ForMember(noteText => noteText.Title, map => map.MapFrom(noteTextDto => noteTextDto.Title));


            CreateMap<NoteHistory, NoteHistoryDto>()
                .ForMember(noteHistoryDto => noteHistoryDto.ID, map => map.MapFrom(noteHistory => noteHistory.ID))
                .ForMember(noteHistoryDto => noteHistoryDto.CreatedDateTime, map => map.MapFrom(noteHistory => noteHistory.CreatedDateTime))
                .ForMember(noteHistoryDto => noteHistoryDto.LastChangesDateTime, map => map.MapFrom(noteHistory => noteHistory.LastChangesDateTime));

            CreateMap<NoteHistoryDto, NoteHistory>()
                .ForMember(noteHistory => noteHistory.ID, map => map.MapFrom(noteHistoryDto => noteHistoryDto.ID))
                .ForMember(noteHistory => noteHistory.CreatedDateTime, map => map.MapFrom(noteHistoryDto => noteHistoryDto.CreatedDateTime))
                .ForMember(noteHistory => noteHistory.LastChangesDateTime, map => map.MapFrom(noteHistoryDto => noteHistoryDto.LastChangesDateTime));

            CreateMap<NoteDesign, NoteDesignDto>()
                .ForMember(noteHistoryDto => noteHistoryDto.ID, map => map.MapFrom(noteHistory => noteHistory.ID))
                .ForMember(noteHistoryDto => noteHistoryDto.Color, map => map.MapFrom(noteHistory => noteHistory.Color))
                .ForMember(noteHistoryDto => noteHistoryDto.NoteID, map => map.MapFrom(noteHistory => noteHistory.NoteID));

            CreateMap<NoteDesignDto, NoteDesign>()
                .ForMember(noteHistory => noteHistory.ID, map => map.MapFrom(noteHistoryDto => noteHistoryDto.ID))
                .ForMember(noteHistory => noteHistory.Color, map => map.MapFrom(noteHistoryDto => noteHistoryDto.Color))
                .ForMember(noteHistory => noteHistory.NoteID, map => map.MapFrom(noteHistoryDto => noteHistoryDto.NoteID));
        }
    }
}
