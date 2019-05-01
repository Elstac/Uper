using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Models;
using WebApp.Models.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApp.Middlewares;
using WebApp.Services;
using WebApp.Models.EmailConfirmation;
using WebApp.Models.ImageManagement;
using WebApp.Models.OfferList;

namespace WebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region Database
            //Configuration of services and test DB required for regirestration and logging in. To skip this change DbBuild value in appseetings.json file
            if(Configuration.GetValue<bool>("DbBuild"))
            {
                //DB configuration
                if (Configuration.GetValue<bool>("Dbbase"))
                {
                    services.AddDbContext<ApplicationContext>(op =>
                 {
                     op.UseSqlite(Configuration.GetConnectionString("TestConnection"));
                 });
                }
                else
                {
                    services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                }
                #endregion

                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();


                services.ConfigureApplicationCookie(op =>
                {
                    op.LoginPath = "/login/signin";
                    op.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

                
                services.Configure<IdentityOptions>(op =>
                {
                    //Configure password requirements
                    op.Password.RequireDigit = false;
                    op.Password.RequiredLength = 5;
                    op.Password.RequireLowercase = true;
                    op.Password.RequireUppercase = false;
                    op.Password.RequireNonAlphanumeric = false;

                    op.User.RequireUniqueEmail = true;
                });

                
            }

            #region SetupDI

            services.AddTransient<ITripDetailsViewModelProvider, TripDetailsViewModelProvider>();

            services.AddTransient<ITripUserRepository, TripUserRepository>();
            services.AddTransient<ITripDetailsRepository, TripDetailsRepository>();
            services.AddTransient<IApplicationUserViewModelGenerator, ApplicationUserViewModelGenerator>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<ITripDetailsCreator,TripDetailsCreator>();
            services.AddTransient<IIdentityResultErrorHtmlCreator,IdentityResultErrorHtmlCreator>();
            services.AddTransient<IEmailAddressValidator,EmailAddressValidator>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IViewerTypeMapper, ViewerTypeMapper>();
            services.AddScoped<ITripDetailsViewModelCreatorFactory, TripDetailViewModelCreatorFactory>();
            services.AddTransient<IAccountEmailConfirmatorFactory, AccountEmailConfirmatorFactory>();
            services.AddTransient<IPasswordResetFactory, PasswordResetFactory>();
            services.AddTransient<IImageSaver, ImageSaver>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<IFileIdProvider, FileIdProvider>();
            services.AddTransient<IImageRemover, ImageRemover>();
            services.AddTransient<IImageManager, PngImageManager>();
            services.AddTransient<IListCreator, ListCreator>();

            #region EmailConfirmation
            services.AddTransient<AccountConfirmationProvider>();
            services.AddTransient<AccountTokenProvider>();
            services.AddTransient<PasswordResetConfirmationProvider>();
            services.AddTransient<PasswordResetTokenProvider>();

            services.AddTransient<Func<ConfirmatorType, IMessageBodyProvider>>((sp)=>
            (type)=>
            {
                switch (type)
                {
                    case ConfirmatorType.Account:
                        return new AccountMessageProvider();
                    case ConfirmatorType.PasswordReset:
                        return new PasswordResetMessageProvider();
                    default:
                        return null;
                }
            });

            services.AddTransient<Func<ConfirmatorType, IConfirmationProvider>>((sp) =>
             (type) =>
             {
                 switch (type)
                 {
                     case ConfirmatorType.Account:
                         return sp.GetService<AccountConfirmationProvider>();
                     case ConfirmatorType.PasswordReset:
                         return sp.GetService<PasswordResetConfirmationProvider>();
                     default:
                         return null;
                 }
             });

            services.AddTransient<Func<ConfirmatorType, IConfirmationTokenProvider>>((sp) =>
             (type) =>
             {
                 switch (type)
                 {
                     case ConfirmatorType.Account:
                         return sp.GetService<AccountTokenProvider>();
                     case ConfirmatorType.PasswordReset:
                         return sp.GetService<PasswordResetTokenProvider>();
                     default:
                         return null;
                 }
             });
            #endregion

            #region EmailServiceSetup
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISmtpClientProvider, GmailSmtpClientProvider>();
            services.AddTransient<IContentBuilder>((fac) =>
            {
                return new ContentBuilder(
                    new System.Text.RegularExpressions.Regex(
                        Configuration.GetValue<string>("MessageTemplateRegex")));
            });

            services.AddTransient<ICredentialsProvider>((fac) =>
            {
                return new CredentialsProvider(Configuration.GetValue<string>("CredentialsFile"));
            });

            services.AddTransient<ITemplateProvider>((fac) =>
            {
                return new JsonTemplateProvider(Configuration.GetValue<string>("TemplateFile"));
            });
            #endregion
            #endregion



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Store instance of the DI service provider so our application can access it anywhere

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            //Custom type mapper for TripDetails redirection
            app.UseTrpeMapper();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Home",
                    template: "Home/Index/{id}/{type}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "tripDetails",
                    template: "TripDetails/Index/{id}/{viewerType}",
                    defaults:new {controller = "TripDetails", action="Index" });

                routes.MapRoute(
                    name: "Profiles",
                    template: "Profiles/Index/{id}/{type}",
                    defaults:new { controller = "Profiles", action = "Index" });

                routes.MapRoute(
                   name: "TripCreator",
                   template: "TripCreator/Index/{id}/{type}",
                   defaults: new { controller = "TripCreator", action = "Index" });

                routes.MapRoute(
                name: "FakeTripList",
                template: "FakeTripList/Index/{id}/{type}",
                defaults: new { controller = "FakeTripList", action = "Index" });
                // TODO Route to user's private profile
                routes.MapRoute(
                    name: "UserProfile",
                    template: "MyProfile");
            });
        }
    }
}
