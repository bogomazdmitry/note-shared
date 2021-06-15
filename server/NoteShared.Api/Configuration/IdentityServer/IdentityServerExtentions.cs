using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoteShared.Infrastructure.Data;
using NoteShared.Infrastructure.Data.Entity.Users;
using System.Reflection;

namespace NoteShared.Api.Configuration.IdentityServer
{
    public static class IdentityServerExtentions
    {
        public static void AddIdentityServer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var identityServerSettingsSection = configuration.GetSection(nameof(IdentityServerSettings));
            var identityServerSettings = identityServerSettingsSection.Get<IdentityServerSettings>();

            services.Configure<IdentityServerSettings>(identityServerSettingsSection);

            var migrationsAssembly = typeof(ApplicationContext).GetTypeInfo().Assembly.GetName().Name;
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryClients(IdentityServerConfiguration.GetClients(identityServerSettings))
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddAspNetIdentity<User>()
                .AddJwtBearerClientAuthentication()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
        }
    }
}
