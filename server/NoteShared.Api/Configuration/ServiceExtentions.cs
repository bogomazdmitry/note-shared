using Microsoft.Extensions.DependencyInjection;
using NoteShared.Services.Interfaces;

namespace NoteShared.Api.Configuration
{
    public static class ServiceExtentions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<INotificationsService, NotificationsService>();
        }
    }
}
