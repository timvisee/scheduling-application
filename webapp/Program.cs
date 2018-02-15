using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
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