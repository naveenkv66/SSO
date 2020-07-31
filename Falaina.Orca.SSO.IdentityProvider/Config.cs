// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Falaina.Orca.SSO.IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope>
       {
            new ApiScope("AspNetWebFormsApp1-DomainApi", "AspNetWebFormsApp1 DomainApi")
       };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "AspNetCoreApp1",
                ClientSecrets = { new Secret("AspNetCoreApp1-Secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:44322/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:44322/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                }
            },

             new Client
                {
                    ClientId = "AspNetWebFormsApp1",
                    ClientName = "AspNetWebFormsApp1",
                    ClientSecrets = { new Secret("AspNetWebFormsApp1t".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    //RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                       "http://192.168.17.26:5969/"
                    },
                    AllowedCorsOrigins ={ "http://192.168.17.26:5969" },
                     // where to redirect to after logout
                    //PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                        //StandardScopes.OpenId.Name,
                        //StandardScopes.Profile.Name,
                        //StandardScopes.Email.Name,
                        //StandardScopes.Roles.Name,
                    }
                },

             new Client
                {
                    ClientId = "ASPNetWebFormApp1_OpenId",
                    ClientName = "ASPNetWebFormApp1_OpenId",
                    ClientSecrets = { new Secret("ASPNetWebFormApp1_OpenId-Secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    //RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:5001/signin-oidc"
                    },

                     // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                        //StandardScopes.OpenId.Name,
                        //StandardScopes.Profile.Name,
                        //StandardScopes.Email.Name,
                        //StandardScopes.Roles.Name,
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,

                    AllowOfflineAccess = true
                },

                //new Client
                //{
                //    ClientId = "blazor",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = false,
                //    RequireClientSecret = false,
                //    AllowedCorsOrigins = { "http://192.168.17.26:5001" },
                //    AllowedScopes = { "openid", "profile", "email", "weatherapi"},
                //    RedirectUris = { "http://192.168.17.26:5001/authentication/login-callback" },
                //    PostLogoutRedirectUris = { "http://192.168.17.26:5001/" },
                //    Enabled = true
                //},
                  new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "http://192.168.17.26:5001" },
                    AllowedScopes = { "openid", "profile", "email", "weatherapi"},
                    RedirectUris = { "http://192.168.17.26:5001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "http://192.168.17.26:5001/" },
                    Enabled = true
                },

                 new Client
                {
                    ClientId = "reactspa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    RequireClientSecret = false,
                     AccessTokenLifetime = 120,
                    AllowedCorsOrigins = { "http://192.168.17.26" },
                    AllowedScopes = { "openid", "profile"},
                    RedirectUris = { "http://192.168.17.26/react/#/callback"},
                   // PostLogoutRedirectUris = { "https://localhost:44333/account/login" },
                    Enabled = true,
                    AllowAccessTokensViaBrowser=true,
                },

                  new Client
                {
                    ClientId = "DashboardServerApp",
                    ClientSecrets = { new Secret("DashboardServerApp".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = false,
                    AllowedCorsOrigins = { "http://localhost:44223" },
                    AllowedScopes = { "openid", "profile"},
                   RedirectUris = { "http://localhost:44223/signin-oidc" },
                   PostLogoutRedirectUris = { "http://localhost:44223/" },

                    Enabled = true
                },

                // new Client
                //{
                //    ClientId = "reactspa",
                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowAccessTokensViaBrowser = true,
                //    RequireConsent = false,
                //    AccessTokenLifetime = 120,
                //    AllowedCorsOrigins = { "https://aashishvanand.me/FalainaSSOReactClientAPP" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,

                //    },
                //    RedirectUris = {
                //        "https://aashishvanand.me/FalainaSSOReactClientAPP/#/callback",
                //        "https://aashishvanand.me/FalainaSSOReactClientAPP/#/silentRenew.html",
                //    },
                //    PostLogoutRedirectUris =
                //    {
                //        "https://aashishvanand.me/FalainaSSOReactClientAPP/#/silentRenew.html"
                //    },

                //    Enabled = true
                //}

        };
    }
}