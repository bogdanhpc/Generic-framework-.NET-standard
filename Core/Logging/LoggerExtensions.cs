using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Core
{
    public static class LoggerExtensions
    {

        public static void LogCriticalSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin="",
            [CallerFilePath] string filePath="",
            [CallerLineNumber] int lineNumber=0,
            params object[] args) => logger.Log(
                LogLevel.Critical, 
                eventId,  
                args.Prepend(origin,filePath,lineNumber, message), 
                exception, 
                LoggerSourceFormatter.Format);

        public static void LogWarningSource(
           this ILogger logger,
           string message,
           EventId eventId = new EventId(),
           Exception exception = null,
           [CallerMemberName] string origin = "",
           [CallerFilePath] string filePath = "",
           [CallerLineNumber] int lineNumber = 0,
           params object[] args) => logger.Log(
               LogLevel.Warning,
               eventId,
               args.Prepend(origin, filePath, lineNumber, message),
               exception,
               LoggerSourceFormatter.Format);

        public static void LogInformationSource(
           this ILogger logger,
           string message,
           EventId eventId = new EventId(),
           Exception exception = null,
           [CallerMemberName] string origin = "",
           [CallerFilePath] string filePath = "",
           [CallerLineNumber] int lineNumber = 0,
           params object[] args) => logger.Log(
               LogLevel.Information,
               eventId,
               args.Prepend(origin, filePath, lineNumber, message),
               exception,
               LoggerSourceFormatter.Format);
        public static void LogErrorSource(
          this ILogger logger,
          string message,
          EventId eventId = new EventId(),
          Exception exception = null,
          [CallerMemberName] string origin = "",
          [CallerFilePath] string filePath = "",
          [CallerLineNumber] int lineNumber = 0,
          params object[] args) => logger.Log(
              LogLevel.Error,
              eventId,
              args.Prepend(origin, filePath, lineNumber, message),
              exception,
              LoggerSourceFormatter.Format);
    }
}
