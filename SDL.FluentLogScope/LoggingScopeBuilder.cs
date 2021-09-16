using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SDL.FluentLogScope
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class LoggingScopeBuilder<TService>
    {
        private const string RequestIdKey = "RequestId";
        
        private const string TraceIdKey = "TraceId";
        
        private const string CorrelationIdKey = "CorrelationId";
        
        private readonly ILogger<TService> _logger;

        private readonly StringBuilder _scopeMessageBuilder;

        private readonly List<object> _scopeValues;

        // ReSharper disable once ContextualLoggerProblem
        internal LoggingScopeBuilder(ILogger<TService> logger)
        {
            _logger = logger;
            
            _scopeMessageBuilder = new StringBuilder();
            _scopeValues = new List<object>();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// Fluently adds a parameter to the logging scope
        /// </summary>
        /// <param name="key">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        /// <returns>Logging scope builder for fluent parameter adding</returns>
        public LoggingScopeBuilder<TService> With(string key, object value)
        {
            _scopeValues.Add(value);
            _scopeMessageBuilder.AppendFormat(" {{{0}}}", key);

            return this;
        }

        /// <summary>
        /// Fluently adds a correlation id to the logging scope
        /// </summary>
        /// <param name="correlationId">The correlation id</param>
        /// <returns>Logging scope builder for fluent parameter adding</returns>
        public LoggingScopeBuilder<TService> WithCorrelationId(Guid correlationId)
            => With(CorrelationIdKey, correlationId);
        
        /// <summary>
        /// Fluently adds a correlation id to the logging scope
        /// </summary>
        /// <param name="correlationId">The correlation id</param>
        /// <returns>Logging scope builder for fluent parameter adding</returns>
        public LoggingScopeBuilder<TService> WithCorrelationId(string correlationId)
            => With(CorrelationIdKey, correlationId);
        
        /// <summary>
        /// Fluently adds a trace id to the logging scope
        /// </summary>
        /// <param name="traceId">The trace id</param>
        /// <returns>Logging scope builder for fluent parameter adding</returns>
        public LoggingScopeBuilder<TService> WithTraceId(string traceId) => With(TraceIdKey, traceId);
        
        /// <summary>
        /// Fluently adds a request id to the logging scope
        /// </summary>
        /// <param name="requestId">The request id</param>
        /// <returns>Logging scope builder for fluent parameter adding</returns>
        public LoggingScopeBuilder<TService> WithRequestId(string requestId) => With(RequestIdKey, requestId);

        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        /// <summary>
        /// Starts the logging scope with all added parameters
        /// </summary>
        /// <returns>Disposable logging scope</returns>
        public IDisposable Begin() 
            => _logger.BeginScope(_scopeMessageBuilder.ToString().TrimStart(), _scopeValues.ToArray());
    }
}