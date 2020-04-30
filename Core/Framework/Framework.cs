using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.IO;

namespace Core
{
    /// <summary>
    /// main entry point into the framework library
    /// </summary>
    public static class Framework
    {
        #region Private memebers

        /// <summary>
        /// Dependecy injection provider 
        /// </summary>
        private static IServiceProvider _serviceProvider;

        #endregion

        #region Public properties

        /// <summary>
        /// Dependecy injection provider 
        /// </summary>
        public static IServiceProvider ServiceProvider => Construction.Provider;

        public static FrameworkConstruction Construction { get; private set; } 

        /// <summary>
        /// Default logger for the framework
        /// </summary>
        public static ILogger Logger => Construction.Provider.GetService<ILogger>();

        public static IConfiguration Configuration => Construction.Provider.GetService<IConfiguration>();

        public static FrameworkEnvironment Environment => Construction.Provider.GetService<FrameworkEnvironment>();

        public static IExceptionHandler ExceptionHandler => Construction.Provider.GetService<IExceptionHandler>();

        #endregion

        #region Public methods
        /// <summary>
        /// Starting point of the framework
        /// </summary>
        public static void Build(this FrameworkConstruction construction)
        {
            //build the service provider
            Construction.Build();

            //Log the Startup complete
            Logger.LogCriticalSource($"Core Framework started in {Environment.Configuration}...");

        }

        public static void Build(this IServiceProvider provider)
        {
            Construction.Build(provider);

            Logger.LogCriticalSource($"Core Framework started in {Environment.Configuration}...");
        }

        #endregion
    }
}
