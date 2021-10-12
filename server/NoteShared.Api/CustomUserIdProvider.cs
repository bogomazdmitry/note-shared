using IdentityModel;
using Microsoft.AspNetCore.SignalR;

namespace Api
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public CustomUserIdProvider() {}

        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(i => i.Type == JwtClaimTypes.Subject)?.Value;
        }
    }
}