// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;


namespace Falaina.Orca.SSO.IdentityProvider
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            //services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();


            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                //.AddSigningCredential(new X509Certificate2(/*your cert*/))
                //.AddSamlPlugin(options => {
                //                    options.Licensee = "/*your license key org name*/";
                //                    options.LicenseKey = "/*your license key*/";
                //                        }
                //            )
                .AddInMemoryIdentityResources(Config.IdentityResources)
                //.AddInMemoryApiResources()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);
               
            services.AddAuthentication()
                .AddAzureAD(options =>
                {
                    options.ClientId = "b3d2a3cf-a428-48ca-92dc-2723f27934a9";
                    options.TenantId = "d20ca167-9360-49a5-b302-710a2706e634";
                    options.Domain = "falainacorp.falainacloud.com";
                    options.CallbackPath = "/signin-oidc";
                    options.Instance = "https://login.microsoftonline.com/";
                }) ;

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";
                options.TokenValidationParameters.ValidateIssuer = true;
                options.SignInScheme = IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme;// Microsoft.AspNetCore.Authentication.AzureAD.UI.AzureADDefaults.AuthenticationScheme;
            });

            

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer()
                //.UseIdentityServerSamlPlugin()
                ;


            //uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
