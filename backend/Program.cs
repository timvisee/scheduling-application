using System;
using System.IO;
using backend.App.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace backend
{
    public class Program
    {
        /// <summary>
        /// Application configuration.
        /// This configuration is used throughout the application to define the applications behaviour.
        /// </summary>
        public static AppConfig AppConfig;


        public static void Main(string[] args)
        {
            try
            {
                // Initialize the application configuration
                Program.AppConfig = new AppConfig(true);

                // Parse the startup arguments, and apply them to the configuration
                AppConfigParameterParser.Parse(args, Program.AppConfig);

                // Set up a new webhost
                var host = new WebHostBuilder()
                    .UseUrls("http://*:5000")
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                // Run the actual web host
                host.Run();
            }
            catch (Exception ex)
            {
                // Print the exception
                Console.WriteLine(ex);

                // Show a warning
                Console.WriteLine(
                    "\n\nAn unrecorerable exception occurred.\nThe application will not quit (code: -1).");

                // Exit
                Environment.Exit(-1);
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            // Get the host from the configuration
            AppConfig config = new AppConfig();

            // Get the host
            String host = config.GetProperty("Web.Hosts");
            if (host == null)
            {
                throw new Exception("Missing 'Web.Hosts' key in configuration, unable to host");
            }

            // Buuld the web host
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(host)
                .Build();
        }

        public static void SetEnvironment(IHostingEnvironment env)
        {
            // Get the host from the configuration
            var config = new AppConfig();

            // Get the host
            var envProperty = config.GetProperty("Environment");

            switch (envProperty)
            {
                case "Development":
                    env.EnvironmentName = EnvironmentName.Development;
                    break;
                case "Staging":
                    env.EnvironmentName = EnvironmentName.Staging;
                    break;
                case "Production":
                    env.EnvironmentName = EnvironmentName.Production;
                    break;
                case null:
                    Console.WriteLine("Missing Environment key in configuration");
                    break;
                default:
                    throw new Exception("Wrong Environment specified in appsettings.json");
            }
        }
    }
}
