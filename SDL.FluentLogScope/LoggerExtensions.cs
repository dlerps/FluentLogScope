using Microsoft.Extensions.Logging;

namespace PeaceOfMind.BE.Core.Logging
{
    public static class LoggerExtensions
    {
        public static LoggingScopeBuilder<TService> Scope<TService>(this ILogger<TService> logger)
            => new LoggingScopeBuilder<TService>(logger);
    }
}