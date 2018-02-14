using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace webapp
{
    /**
     * This configuration is used to manage the application configuration,
     * loaded from configuration files, and environment variables.
     */
    public class AppConfig
    {
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
                return envVar;

            // Get the key from the configuration
            String configVar = this.config[configKey];
            if (configVar != null)
                return this.config[configKey];

            // No property was found, throw an error
            throw new Exception("Missing configuration key '" + key + "'");
        }
    }
}
