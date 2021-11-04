using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NoteShared.Api.Configuration;
using NoteShared.Api.Configuration.Identity;
using NoteShared.Api.Configuration.IdentityServer;
using NoteShared.Api.Hubs;
using NoteShared.Infrastructure.Data;
using Serilog;
using System;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc
                );

            if (_hostEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen();
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(e => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(Configuration.GetValue<string>("ClientUrl"))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });
            }


            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity(Configuration);

            services.AddIdentityServer(Configuration, _hostEnvironment);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddLocalApi(options =>
                {
                    options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName;
                    options.SaveToken = true;
                })
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Configuration.GetValue<string>("ServerUrl");
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = IdentityServerConstants.LocalApi.ScopeName;
                    options.TokenRetriever = new Func<HttpRequest, string>(req =>
                    {
                        var fromHeader = TokenRetrieval.FromAuthorizationHeader();
                        var fromQuery = TokenRetrieval.FromQueryString();
                        var result = fromHeader(req) ?? fromQuery(req);
                        return result;
                    });
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme)
                        .RequireClaim("scope", "openid");
                });
            });

            services.AddRepositoreis();

            services.AddServices();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSignalR();

            services.AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoteShared");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllers();
                e.MapHub<NoteHub>("hub/note");
                e.MapHub<NotificationsHub>("hub/notifications");
            });
        }
    }
}
