using Microsoft.Extensions.DependencyInjection;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.Users;
using NoteShared.Infrastructure.Data.Repositories;

namespace NoteShared.Api.Configuration
{
    public static class RepositoryExtentions
    {
        public static void AddRepositoreis(this IServiceCollection services)
        {
            services.AddScoped<IRepositioryUser, RepositoryUser>();
            services.AddScoped<IRepositoryNotes, RepositoryNotes>();
            services.AddScoped<IRepositoryNoteDesigns, RepositoryNoteDesigns>();
            services.AddScoped<IRepositoryNoteHistories, RepositoryNoteHistories>();
        }
    }
}
