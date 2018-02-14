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

        public static IWebHost BuildWebHost(string[] args) =>
            // Get the host from the configuration
            AppConfig config = new AppConfig();

            // Get the host
            String host = config.GetProperty("Web.Hosts");
            if (host == null) {
                throw new Exception("Missing 'Web.Hosts' key in configuration, unable to host");
            }

            // Buuld the web host
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(host)
                .Build();
    }
}
