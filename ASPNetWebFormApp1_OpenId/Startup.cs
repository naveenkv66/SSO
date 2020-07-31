/*
using IdentityModel;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ASPNetWebFormApp1_OpenId.Startup))]

namespace ASPNetWebFormApp1_OpenId
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string clientId = "ASPNetWebFormApp1_OpenId";
            string clientSecret = "ASPNetWebFormApp1_OpenId-Secret";
            string redirectUri = "https://localhost:44372/signin-oidc";
            string postLogoutRedirectUri = "";

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/Login.aspx"),

                // Configure SameSite as needed for your app. Lax works well for most scenarios here but
                // you may want to set SameSiteMode.None for HTTPS
                CookieSameSite = SameSiteMode.None,
                CookieSecure = CookieSecureOption.SameAsRequest
            });

            // Configure Auth0 authentication
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:5001/",

                ClientId = clientId,
                ClientSecret = clientSecret,

                RedirectUri = redirectUri,
                PostLogoutRedirectUri = postLogoutRedirectUri,

                ResponseType = OpenIdConnectResponseType.IdToken,
                Scope = "openid profile",

                RequireHttpsMetadata = false,
                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",
                

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async n =>
                    {
                        var tokenClient = new TokenClient(
                            n.Options.Authority + "connect/token",
                            clientId,
                            clientSecret
                        );

                        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(n.Code, n.RedirectUri);

                        if (tokenResponse.IsError)
                        {
                            throw new AuthenticationException(tokenResponse.Error);
                        }

                        var userInfoClient = new UserInfoClient("https://localhost:5001/" + "connect /userinfo");

                        var userInfoResponse = await userInfoClient.GetAsync(tokenResponse.AccessToken);

                        var id = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType);
                        id.AddClaims(userInfoResponse.Claims);

                        var Name = userInfoResponse.Claims.FirstOrDefault(c => c.Type.Equals("Name", StringComparison.CurrentCultureIgnoreCase)).Value;
                        id.AddClaim(new Claim("access_token", tokenResponse.AccessToken));
                        id.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToLocalTime().ToString(CultureInfo.InvariantCulture)));
                        id.AddClaim(new Claim("refresh_token", tokenResponse.RefreshToken));
                        id.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        id.AddClaim(new Claim("sid", n.AuthenticationTicket.Identity.FindFirst("sid").Value));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            new ClaimsIdentity(id.Claims, n.AuthenticationTicket.Identity.AuthenticationType, JwtClaimTypes.Name, JwtClaimTypes.Role),
                            n.AuthenticationTicket.Properties
                        );

                        List<Claim> _claims = new List<Claim>();
                        _claims.AddRange(new List<Claim>
                        {
                            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Name),
                            new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",Name)
                        });

                    },
                }
            });
        }
    }
}

*/


using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;

[assembly: OwinStartup(typeof(ASPNetWebFormApp1.Startup1))]
namespace ASPNetWebFormApp1
{
    public class Startup1
    {
        private readonly string _clientId = "ASPNetWebFormApp1_OpenId";

        private readonly string _redirectUri = "https://localhost:44372/signin-oidc";
        //private readonly string _authority = "https://localhost:5001/";
        private readonly string _clientSecret = "ASPNetWebFormApp1_OpenId-Secret";

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Authority = "https://localhost:5001/",
                RedirectUri = _redirectUri,
                ResponseType = OpenIdConnectResponseType.IdToken,
                //UsePkce = true,
                Scope = OpenIdConnectScope.OpenIdProfile,
                //TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" },
                
                //Notifications = new OpenIdConnectAuthenticationNotifications
                //{
                //    AuthorizationCodeReceived = async n =>
                //    {
                //        // Exchange code for access and ID tokens
                //        var tokenClient = new TokenClient("https://localhost:5001/v1/token", _clientId, _clientSecret);

                //        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(n.Code, _redirectUri);
                //        if (tokenResponse.IsError)
                //        {
                //            throw new Exception(tokenResponse.Error);
                //        }

                //        var userInfoClient = new UserInfoClient("https://localhost:5001/v1/userinfo");
                //        var userInfoResponse = await userInfoClient.GetAsync(tokenResponse.AccessToken);

                //        var claims = new List<Claim>(userInfoResponse.Claims)
                //                          {
                //                            new Claim("id_token", tokenResponse.IdentityToken),
                //                            new Claim("access_token", tokenResponse.AccessToken)
                //                          };

                //        n.AuthenticationTicket.Identity.AddClaims(claims);
                //    },
                //},
            });
        }
    }
}


