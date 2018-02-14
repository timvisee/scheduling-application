using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // TODO: This is used to serve frontend files, use something better
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "../frontend/")
                ),
                RequestPath = ""
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "api",
                    template: "api/v1/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
