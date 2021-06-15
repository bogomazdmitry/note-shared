using Microsoft.Extensions.DependencyInjection;
using NoteShared.Services.Interfaces;

namespace NoteShared.Api.Configuration
{
    public static class ServiceExtentions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<NoteService>();
        }
    }
}
