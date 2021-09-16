using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace SDL.FluentLogScope.Tests
{
    public class LoggerExtensions_When_scope
    {
        private readonly ILogger<DummyService> _logger;

        public LoggerExtensions_When_scope()
        {
            _logger = Mock.Of<ILogger<DummyService>>();
        }

        [Fact]
        public void Return_logging_scope_builder()
        {
            // Act
            var builder = _logger.Scope();
            
            // Assert
            builder.Should().NotBeNull();
            builder.Should().BeOfType<LoggingScopeBuilder<DummyService>>();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once ClassNeverInstantiated.Global
        public class DummyService { }
    }
}