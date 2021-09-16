using Microsoft.Extensions.Logging;

namespace SDL.FluentLogScope
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Starts a logging scope fluently. Use With(...) to add parameters and .Begin() to begin the scope.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <typeparam name="TService">Service type of the logger</typeparam>
        /// <returns>Fluent builder for logging scope</returns>
        public static LoggingScopeBuilder<TService> Scope<TService>(this ILogger<TService> logger)
            => new LoggingScopeBuilder<TService>(logger);
    }
}