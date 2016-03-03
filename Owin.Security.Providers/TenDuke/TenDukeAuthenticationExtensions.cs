using System;

namespace Owin.Security.Providers.TenDuke
{
    public static class TenDukeAuthenticationExtensions
    {
        public static IAppBuilder UseTenDukeAuthentication(this IAppBuilder app,
            TenDukeAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");

            app.Use(typeof(TenDukeAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseTenDukeAuthentication(this IAppBuilder app, string baseUrl, string clientId, string clientSecret, string grantType, string licenseRequests)
        {
            return app.UseTenDukeAuthentication(new TenDukeAuthenticationOptions
            {
                BaseUrl = baseUrl,
                ClientId = clientId,
                ClientSecret = clientSecret,
                GrantType = grantType,
                LicenseRequests = licenseRequests
            });
        }
    }
}