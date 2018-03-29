using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace backend
{
    /**
     * This configuration is used to manage the application configuration,
     * loaded from configuration files, and environment variables.
     */
    public class AppConfig
    {
        /// <summary>
        /// True to reset the database on startup, false to use the existing database.
        /// </summary>
        public bool DatabaseReset = false;

        /// <summary>
        /// True to exit the application after the database is reset, false to keep the application running.
        /// The application will never exit if the database is not being reset.
        /// </summary>
        public bool DatabaseExitAfterReset = false;

        /**
         * A configuration builder, that imports configuration properties from
         * files on the system.
         */
        private IConfigurationRoot config;

        /**
         * Constructor.
         * Loads the application configuration.
         */
        public AppConfig()
        {
            // Load the configuration
            Load();
        }

        /**
         * Load the configuration files.
         */
        void Load()
        {
            // Create a configuration buider
            this.config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /**
         * Get a property from the configuration.
         */
        public String GetProperty(String key)
        {
            // Generate the config and environment variable key
            String envKey = key.Replace(".", "_").ToUpper();
            String configKey = key.Replace(".", ":");

            // Get the property from an environment variable
            String envVar = Environment.GetEnvironmentVariable(envKey);
            if (envVar != null)
            {
                Console.WriteLine("Using envVar " + envKey + ": " + envVar);
                return envVar;
            }

            // Get the key from the configuration
            String configVar = this.config[configKey];
            if (configVar != null)
            {
                Console.WriteLine("Using configVar " + configKey + ": " + configVar);
                return this.config[configKey];
            }

            // No property was found, throw an error
            throw new Exception("Missing configuration key '" + key + "'");
        }

        /**
         * Generate the connection string to use for the database.
         */
        public String GenerateDbConnectionString()
        {
            // Generate a connection string
            return "Data Source=" + this.GetProperty("Database.Host") + ";"
                   + "Initial Catalog=" + this.GetProperty("Database.Database") + ";"
                   + "Integrated Security=False;User ID=" + this.GetProperty("Database.User") + ";"
                   + "Password=" + this.GetProperty("Database.Password") + ";"
                   + "Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;"
                   + "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="init">True to immediately initialize the configuration, false if not.</param>
        public AppConfig(bool init)
        {
            // Initialize
            if (init)
                Init();
        }

        /// <summary>
        /// Configuration initialization code.
        /// </summary>
        public void Init()
        {
            // Add configuration initialization code here, such as environment variable parsing.
        }
    }
}
