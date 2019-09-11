using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using InvTracker.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(InvTracker.Startup))]
namespace InvTracker
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            ConfigureOAuth(app); //Configure OAuth
        }
        private void ConfigureOAuth(IAppBuilder app)
        {
            // OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            // {
            //     AllowInsecureHttp = true,
            //     TokenEndpointPath = new PathString("/token"),
            //     AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //     Provider = new SimpleAuthorizationServerProvider()
            // };
            // //Token Generation


            //// app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            // app.UseOAuthAuthorizationServer(oAuthServerOptions);
            // app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            app.CreatePerOwinContext<AuthDbContext>(() => new AuthDbContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new SimpleAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,

            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {

               

                //using (UserRepository _repo = new UserRepository())
                //{
                //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                //    if (user == null)
                //    {
                //        context.SetError("invalid_grant", "The user name or password is incorrect.");
                //        return;
                //    }
                //}

                //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                //identity.AddClaim(new Claim("sub", context.UserName));
                //identity.AddClaim(new Claim("role", "user"));

                //context.Validated(identity);
                UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user;
                try
                {
                    using (UserRepository _repo = new UserRepository())
                    {
                        user = await _repo.FindUser(context.UserName, context.Password);
                    }
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                            user,
                                                            DefaultAuthenticationTypes.ApplicationCookie);
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Invalid UserId or password'");
                    context.Rejected();
                }

            }
        }

        private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<IdentityUser>(context.Get<AuthDbContext>());
            var owinManager = new UserManager<IdentityUser>(userStore);
            return owinManager;
        }
    }
}