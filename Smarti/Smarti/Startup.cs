using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smarti.Data;
using Smarti.Models;
using Smarti.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using Hangfire.Storage;
using uPLibrary.Networking.M2Mqtt;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Smarti
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Signin settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "SmartiCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<ISocketRepository, SocketRepository>();
            services.AddTransient<ISocketDataRepository, SocketDataRepository>();
            services.AddTransient<ITimeTaskRepository, TimeTaskRepository>();
            services.AddTransient<IChartGenerator, ChartGenerator>();
            services.AddTransient<IMqttAppClient, MqttAppClient>();
            services.AddSingleton<IMqttAppClientSingleton, MqttAppClientSingleton>();

            //Resource based authorization
            services.AddTransient<IAuthorizationHandler, RoomAuthorizationCrudHandler>();
            services.AddTransient<IAuthorizationHandler, SocketAuthorizationCrudHandler>();
            services.AddTransient<IAuthorizationHandler, TimeTaskAuthorizationCrudHandler>();

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            //Register for secrets
            services.Configure<AuthMessageSenderOptions>(Configuration);

            // HangFire for background tasks
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();

            services.AddMvc()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("/Views/Shared/PartialViews/{0}.cshtml");
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseExceptionHandler("/Error/Index");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            dbInitializer.Initialize();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Socket}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "TimeTask",
                   template: "TimeTask/{id?}",
                   defaults: new { controller = "TimeTask", action = "Index" }
                );
            });
        }
    }
}
