using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VExam.Api.Services.Departments;
using VExam.Api.Services.JobTitle;
using VExam.Api.Services.Question;
using VPortal.Core.DIExtensions;
using VPortal.TokenManager;

namespace VExam.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
             // Add framework services.
            services.AddSingleton(Configuration);
            var serviceProvider = services.BuildServiceProvider();

            // The services of the core engine are injected here
            // core engine has services related to database and logger
            services.RegisterVPortalCoreServices(serviceProvider);
            services.AddMvc();
            services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AutomaticChallenge = true;
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Bearer";
            });
            services.AddMvcCore();
            
            //custom services...
            services.AddTransient<IDepartmentService,DepartmentService>();
            services.AddTransient<IJobTitleService,JobTitleService>();
            services.AddTransient<IQuestionService,QuestionService>();
            
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

//app.UseIdentity();
            Register(app);
            app.UseMvc();
         
            
        }
        public void Register(IApplicationBuilder app)
        {
         
            string issuer =Configuration.GetSection("TokenAuthentication:Issuer").Value;
            string audience = Configuration.GetSection("TokenAuthentication:Audience").Value;
            var secret = Encoding.UTF8.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value);
            
            var tokenProviderOptions = new TokenProviderOptions
            {
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            };
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Bearer",
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateLifetime = true

                }
            });
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomTokenFormat(
                   SecurityAlgorithms.HmacSha256,
                    new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateLifetime = true

                    })
            });
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
           // app.UseIdentity();
        }
        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            // DEMO CODE
            Console.WriteLine(username);
            if (username == "TEST" && password == "TEST123")
            {
                Console.WriteLine("authenticated");
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Account doesn't exists
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
