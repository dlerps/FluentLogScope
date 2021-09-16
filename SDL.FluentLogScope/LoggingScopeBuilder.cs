using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PeaceOfMind.BE.Core.Logging
{
    public class LoggingScopeBuilder<TService>
    {
        private const string IdKey = "Id";
        
        private const string CorrelationIdKey = "CorrelationId";
        
        private const string UserIdKey = "UserId";
        
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
        public LoggingScopeBuilder<TService> With(string key, object value)
        {
            _scopeValues.Add(value);
            _scopeMessageBuilder.AppendFormat(" {{{0}}}", key);

            return this;
        }

        public LoggingScopeBuilder<TService> WithCorrelationId(Guid correlationId) 
            => With(CorrelationIdKey, correlationId);
        
        public LoggingScopeBuilder<TService> WithUserId(Guid userId) => With(UserIdKey, userId);
        
        public LoggingScopeBuilder<TService> WithId(Guid id) => With(IdKey, id);
        
        public LoggingScopeBuilder<TService> WithId(int id) => With(IdKey, id);
        
        public LoggingScopeBuilder<TService> WithId(string id) => With(IdKey, id);

        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        public IDisposable Begin() 
            => _logger.BeginScope(_scopeMessageBuilder.ToString().TrimStart(), _scopeValues.ToArray());
    }
}