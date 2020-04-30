using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Core
{
    /// <summary>
    /// Extension methods for the framework
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// Adds a default logger for a non-generic ILogger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddDefaultLogger(this FrameworkConstruction construction)
        {
            construction.Services.AddLogging(options =>
            {
                //TODO: setup logger from congiguration
                options.AddConfiguration(construction.Configuration.GetSection("Logging"));
                //console logger
                options.AddConsole();

                //dubug logger
                options.AddDebug();

                //TODO: file logger
                //options.AddFile("log.txt");
            });


            construction.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Core"));
            return construction;
        }

        public static FrameworkConstruction Configure(this FrameworkConstruction construction, Action<IConfigurationBuilder> configure = null)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{construction.Environment.Configuration}.json", optional: true, reloadOnChange: true);


            configure?.Invoke(configurationBuilder);

            var configuration = configurationBuilder.Build();
            construction.Services.AddSingleton<IConfiguration>(configuration);

            construction.Configuration = configuration;
            return construction;
        }

        public static FrameworkConstruction UseDefaultServices(this FrameworkConstruction construction)
        {
            construction.AddDefaultExceptionHandler();

            construction.AddDefaultLogger();

            return construction;
        }

        public static FrameworkConstruction AddDefaultExceptionHandler(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IExceptionHandler>(new BaseExceptionHandler());
            return construction;
        }

        
    }
}
