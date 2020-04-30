
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Core
{
    public class FileLoggerProvider : ILoggerProvider
    {
        protected readonly FileLoggerConfiguration mConfiguration;
        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();
        protected string mPath;

        public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
        {
            mConfiguration = configuration;
            mPath = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return mLoggers.GetOrAdd(categoryName, name => new FileLogger(name, mPath, mConfiguration));
        }

        public void Dispose()
        {
            mLoggers.Clear();
        }
    }
}
