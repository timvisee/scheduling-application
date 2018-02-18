using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using webapp.Models;

namespace webapp
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

            Console.Write(Copyright);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var dbConnection = new AppConfig().GenerateDbConnectionString();
            services.AddDbContext<DbEntity>(options => options.UseSqlServer(dbConnection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Set the environment based on the appsettings.json
            Program.SetEnvironment(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Serve frontend and backend files
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "../frontend/")
                ),
                RequestPath = ""
            });
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
            foreach(String host in allowedHosts)
                Console.WriteLine("Allowing CORS request for: {0}", host);

            // Configure CORS with the proper hosts
            // TODO: use the specific host logic below, ensure it works
            //app.UseCors(corsPolicyBuilder =>
            //    corsPolicyBuilder.WithOrigins(allowedHosts)
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //);
            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            // Define the routes
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
