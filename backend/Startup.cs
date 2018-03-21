using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using backend.Models;
using backend.Data;
using backend.Services;
using backend.Types;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            const string Copyright = " BBBBBBBB   EEEEEEEE  UU     UU  NN    NN        IIII    TTTTTTTT\n" +
                                     " BB     BB  EE        UU     UU  NNN   NN        II        TT    \n" +
                                     " BB     BB  EE        UU     UU  NNNN  NN       II        TT     \n" +
                                     " BBBBBBBB   EEEEEE    UU     UU  NN NN NN      II        TT      \n" +
                                     " BB     BB  EE        UU     UU  NN  NNNN     II        TT       \n" +
                                     " BB     BB  EE        UU     UU  NN   NNN    II        TT        \n" +
                                     " BBBBBBBB   EEEEEEEE   UUUUUUU   NN    NN  IIII       TT         \n\n";

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(Copyright);
            Console.ResetColor();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnection = new AppConfig().GenerateDbConnectionString();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = new AppConfig().GetProperty("Web.ClientId");
                googleOptions.ClientSecret = new AppConfig().GetProperty("Web.ClientSecret");
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            // Set the environment based on the appsettings.json
            Program.SetEnvironment(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            // Serve backend files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot")
                ),
                RequestPath = ""
            });

            // Determine which hosts are allowed, for proper CORS configuration
            AppConfig config = new AppConfig();
            String[] allowedHosts = config.GetProperty("Web.AllowedHosts").Split(',');

            // Report the hosts we're allowing CORS on
            foreach (String host in allowedHosts)
                Console.WriteLine("Allowing CORS request for: {0}", host);

            // Configure CORS with the proper hosts
            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.WithOrigins(allowedHosts)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            // TODO: remove this when we're done with it
            /* // Create a new example group */
            /* var group = new Group(); */
            /* group.Name = "My new fancy group"; */
            /* context.Groups.Add(group); */
            /* context.SaveChanges(); */

            // Define the routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            // TODO: reenable this once the DbBuilder is complete
            // Seed database if not running in production
//            if (Program.AppConfig.DatabaseReset)
//                DbBuilder.Rebuild(context);
        }
    }
}
