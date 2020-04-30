using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Core
{
    public class FileLogger : ILogger
    {
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();
        private string mCategoryName;
        private string mPath;
        private FileLoggerConfiguration mConfiguration;

        public FileLogger(string categoryName, string mPath, FileLoggerConfiguration mConfiguration)
        {
            this.mCategoryName = categoryName;
            this.mPath = mPath;
            this.mConfiguration = mConfiguration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= mConfiguration.LogLevel;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var timeLogString = mConfiguration.LogTime ? $"[{currentTime}]" : "";

            var message = formatter(state, exception);
            var output = $"{timeLogString}{message}{Environment.NewLine}";

            var normalisedPath = Path.GetFullPath(mPath).ToUpper();
            var fileLock = FileLocks.GetOrAdd(normalisedPath, path => new object());

            lock (fileLock)
            {
                File.AppendAllText(normalisedPath, output);
            }
        }
    }
}