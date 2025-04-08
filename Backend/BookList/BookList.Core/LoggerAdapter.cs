using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace BookList.Core
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(string? message, [CallerMemberName] string memberName = "", params object?[] args)
        {
            _logger.LogError(message, args);
            LogWithCallerInfo("ERROR", message, memberName, args);
        }

        public void LogError(string? message, Exception? exception, [CallerMemberName] string memberName = "", params object?[] args)
        {
            _logger.LogError(exception, message, args);
            LogWithCallerInfo("ERROR", message, memberName, args, exception);
        }

        public void LogWarning(string message, [CallerMemberName] string memberName = "", params object?[] args)
        {
            _logger.LogWarning(message, args);
            LogWithCallerInfo("WARNING", message, memberName, args);
        }

        public void LogCritical(string message, Exception? exception, [CallerMemberName] string memberName = "", params object?[] args)
        {
            _logger.LogCritical(exception, message, args);
            LogWithCallerInfo("CRITICAL", message, memberName, args, exception);
        }

        public void LogInformation(string message, [CallerMemberName] string memberName = "", params object?[] args)
        {
            _logger.LogInformation(message, args);
            LogWithCallerInfo("INFO", message, memberName, args);
        }


        private void LogWithCallerInfo(string logLevel, string message, string memberName, object?[] args, Exception? exception = null)
        {
            string logMessage = $"[{logLevel}] {message} in {memberName}";

            if (exception != null)
            {
                logMessage += $" Exception: {exception.Message}";
            }

            _logger.Log(logLevel switch
            {
                "ERROR" => LogLevel.Error,
                "WARNING" => LogLevel.Warning,
                "CRITICAL" => LogLevel.Critical,
                "INFO" => LogLevel.Information,
                _ => LogLevel.Information
            }, exception, logMessage, args);
        }
    }
}
