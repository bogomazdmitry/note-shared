using Microsoft.Extensions.DependencyInjection;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.Users;
using NoteShared.Infrastructure.Data.Repositories;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;
using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entities.Notifications;
using NoteShared.Infrastructure.Data.Repositoreis;

namespace NoteShared.Api.Configuration
{
    public static class RepositoryExtentions
    {
        public static void AddRepositoreis(this IServiceCollection services)
        {
            services.AddScoped<IRepositioryUsers, RepositoryUsers>();
            services.AddScoped<IRepositoryNotes, RepositoryNotes>();
            services.AddScoped<IRepositoryNoteHistories, RepositoryNoteHistories>();
            services.AddScoped<IRepositoryNoteDesigns, RepositoryNoteDesigns>();
            services.AddScoped<IRepositoryNoteTexts, RepositoryNoteTexts>();
            services.AddScoped<IRepositoryNotifications, RepositoryNotifications>();
        }
    }
}
