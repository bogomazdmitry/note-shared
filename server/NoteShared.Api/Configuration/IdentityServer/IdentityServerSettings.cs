namespace NoteShared.Api.Configuration.IdentityServer
{
    public class IdentityServerSettings
    {
        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public string AllowedCorsOrigins { get; set; }

        public string AppId { get; set; }
    }
}
