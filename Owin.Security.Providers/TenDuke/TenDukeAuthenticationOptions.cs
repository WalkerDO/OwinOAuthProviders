using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Owin.Security.Providers.TenDuke
{
    public class TenDukeAuthenticationOptions : AuthenticationOptions
    {
        public class TenDukeAuthenticationEndpoints
        {
            /// <summary>
            /// Endpoint which is used to redirect users to request TenDuke access
            /// </summary>
            /// <remarks>
            /// Defaults to https://causeway-idp-dev.10duke.com/oauth2/authz/
            /// </remarks>
            public string AuthorizationEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to exchange code for access token
            /// </summary>
            /// <remarks>
            /// Defaults to https://causeway-idp-dev.10duke.com/oauth2/access
            /// </remarks>
            public string TokenEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to obtain user information after authentication
            /// </summary>
            /// <remarks>
            /// Defaults to https://causeway-idp-dev.10duke.com/graph/me().json
            /// </remarks>
            public string UserInfoEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to obtain user licenses after authentication
            /// </summary>
            /// <remarks>
            /// Defaults to https://causeway-idp-dev.10duke.com/authz/.jwt
            /// </remarks>
            public string LicenseRequestEndpoint { get; set; }
        }

        private const string AuthorizationEndPoint = "https://causeway-idp-dev.10duke.com/oauth2/authz/";
        private const string TokenEndpoint = "https://causeway-idp-dev.10duke.com/oauth2/access";
        private const string UserInfoEndpoint = "https://causeway-idp-dev.10duke.com/graph/me().json";
        private const string LicenseRequestEndpoint = "https://causeway-idp-dev.10duke.com/authz/.json";

        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to GitHub.
        /// </summary>
        /// <value>
        ///     The pinned certificate validator.
        /// </value>
        /// <remarks>
        ///     If this property is null then the default certificate checks are performed,
        ///     validating the subject name and if the signing chain is a trusted party.
        /// </remarks>
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        /// <summary>
        ///     The HttpMessageHandler used to communicate with GitHub.
        ///     This cannot be set at the same time as BackchannelCertificateValidator unless the value
        ///     can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with GitHub.
        /// </summary>
        /// <value>
        ///     The back channel timeout in milliseconds.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        ///     The request path within the application's base path where the user-agent will be returned.
        ///     The middleware will process this request when it arrives.
        ///     Default value is "/signin-github".
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        ///     Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        ///     Gets or sets the GitHub supplied Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the GitHub supplied Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        public string GrantType { get; set; }

        public string LicenseRequests { get; set; }

        /// <summary>
        /// Gets the sets of OAuth endpoints used to authenticate against GitHub.  Overriding these endpoints allows you to use GitHub Enterprise for
        /// authentication.
        /// </summary>
        public TenDukeAuthenticationEndpoints Endpoints { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="IGitHubAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public ITenDukeAuthenticationProvider Provider { get; set; }

        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; private set; }

        /// <summary>
        ///     Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user
        ///     <see cref="System.Security.Claims.ClaimsIdentity" />.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        ///     Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        /// <summary>
        ///     Initializes a new <see cref="GitHubAuthenticationOptions" />
        /// </summary>
        public TenDukeAuthenticationOptions()
            : base("TenDuke")
        {
            Caption = Constants.DefaultAuthenticationType;
            CallbackPath = new PathString("/signin-tenduke");
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>
            {
                "email"
            };
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            Endpoints = new TenDukeAuthenticationEndpoints
            {
                AuthorizationEndpoint = AuthorizationEndPoint,
                TokenEndpoint = TokenEndpoint,
                UserInfoEndpoint = UserInfoEndpoint,
                LicenseRequestEndpoint = LicenseRequestEndpoint
            };
        }
    }
}