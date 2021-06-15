using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.EntityFrameworkCore;
using NoteShared.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;
using IdentityServer4;
using NoteShared.Api.Configuration;
using NoteShared.Api.Configuration.IdentityServer;
using NoteShared.Api.Configuration.Identity;

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
            services.AddControllers();

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddLocalApi(options =>
                {
                    options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName;
                    options.SaveToken = true;
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
            app.UseAuthorization();

            app.UseEndpoints(e => 
            {
                e.MapControllers();
            });
        }
    }
}
