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
using VExam.Services.Departments;
using VExam.Services.JobTitle;
using VExam.Services.Question;
using VPortal.Core.DIExtensions;
using VPortal.TokenManager;
using Newtonsoft.Json.Serialization;
using VExam.Services.Interviewees;
using VExam.Services.DAL;
using VExam.Services.QuestionCategories;
using VExam.Services.QuestionComplexities;
using VExam.Services.QuestionTypes;
using VExam.Services.QuestionsforSets;
using VExam.Services.SessionwiseJobs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using VExam.Services.Users;

namespace VExam.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
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

            // By default the json result from WebApi is camelCased, so convert it into PascalCase for easiness using SerializerSettings
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AutomaticChallenge = true;
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Bearer";
            });
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("Interviewee",
                    policy =>
                    {
                        policy.RequireClaim(ClaimTypes.Actor, "Interviewee");
                    });

                    options.AddPolicy("User",
                    policy =>
                    {
                        policy.RequireClaim(ClaimTypes.Actor, "User");
                    });
                });

            services.AddMvcCore().AddJsonFormatters();
            services.AddCors();

            //custom services...
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IJobTitleService, JobTitleService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IIntervieweeService, IntervieweeService>();
            services.AddTransient<IQuestionCategoryService, QuestionCategoryService>();
            services.AddTransient<IQuestionComplexityService, QuestionComplexityService>();
            services.AddTransient<IQuestionTypeService, QuestionTypeService>();
            services.AddTransient<IQuestionsforSetService, QuestionsforSetService>();
            services.AddTransient<ISessionwiseJobsService, SessionwiseJobsService>();
            services.AddTransient<IUserService,UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseIdentity();
            Register(app);

            app.UseCors(builder =>
                    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();


        }
        public void Register(IApplicationBuilder app)
        {
            string issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value;
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
            var userTokenProvider = new TokenProviderOptions
            {
                Path = "/api/v1/user/token",
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetUserIdentity
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
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(userTokenProvider));
            // app.UseIdentity();
        }

        private Task<ClaimsIdentity> GetIdentity(string emailaddress, string contactnumber)
        {
            Console.WriteLine("interviewee identity called");
            var result = Interviewees.IntervieweeValidationAsync(emailaddress, contactnumber).Result;
            if (result)
            {
                var intervieweeDetail = Interviewees.GetIntervieweeDetailAsync(emailaddress, contactnumber).Result;
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(emailaddress, "EmailAddress"),
                new Claim[] {
                    new Claim("EmailAddress",intervieweeDetail.EmailAddress),
                    new Claim("IntervieweeId",intervieweeDetail.IntervieweeId.ToString()),
                    new Claim("InterviewSessionId",intervieweeDetail.InterviewSessionId.ToString()),
                    new Claim("JobTitleId",intervieweeDetail.JobTitleId.ToString()),
                    new Claim(ClaimTypes.Actor,"Interviewee")
                 }));
            }

            // Account doesn't exists
            return Task.FromResult<ClaimsIdentity>(null);
        }
        private Task<ClaimsIdentity> GetUserIdentity(string emailAddress, string password)
        {

            Console.WriteLine("user identity called");

            string savedPassword = Users.GetUserPasswordAsync(emailAddress).Result;
            bool IsPasswordValid = PasswordManager.ValidateBcrypt(emailAddress, password, savedPassword);
            if (IsPasswordValid)
            {
            var userDetails = Users.GetUserDetailAsync(emailAddress).Result;
             return Task.FromResult(new ClaimsIdentity(new GenericIdentity(emailAddress, "EmailAddress"),
               new Claim[] {
                    new Claim(ClaimTypes.Email,emailAddress),
                    new Claim(ClaimTypes.Role,userDetails.RoleId),
                    new Claim("Department",userDetails.DepartmentId.ToString()),
                    new Claim("UserId",userDetails.UserId.ToString()),
                    new Claim(ClaimTypes.Actor,"User")
                }));

            }
            //  Account doesn't exists
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
