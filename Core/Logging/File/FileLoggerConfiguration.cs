using Microsoft.Extensions.Logging;

namespace Core
{
    public class FileLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        public bool LogTime { get; set; } = true;

        public string FilePath { get; set; }
    }
}
