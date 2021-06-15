using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteShared.Infrastructure.Data;
using NoteShared.Infrastructure.Data.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteShared.Api.Configuration.Identity
{
    public static class IdentityExtention
    {
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(900);
                options.Lockout.MaxFailedAccessAttempts = 20;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();
        }
    }
}
