using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace SDL.FluentLogScope.Tests
{
    public class LoggingScopeBuilder_When_building
    {
        // class under test
        private readonly LoggingScopeBuilder<DummyService> _builder;

        private readonly Mock<ILogger<DummyService>> _logger;

        public LoggingScopeBuilder_When_building()
        {
            _logger = new Mock<ILogger<DummyService>>();
            _builder = new LoggingScopeBuilder<DummyService>(_logger.Object);
        }

        [Fact]
        public void Then_return_disposable_scope()
        {
            // Arrange
            var scope = Mock.Of<IDisposable>();

            _logger
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                .Setup(log => log.BeginScope(It.IsAny< IReadOnlyCollection<KeyValuePair<string, object>>>()))
                .Returns(scope);
            
            // Act
            var loggingScope = _builder.Begin();
            
            // Assert
            loggingScope.Should().NotBeNull();
            loggingScope.Should().Be(scope);
        }
        
        [Fact]
        public void Then_begin_scope_with_encapsulated_logger()
        {
            // Act
            _builder.Begin();
            
            // Assert
            _logger.Verify(
                log => log.BeginScope(It.IsAny<IReadOnlyCollection<KeyValuePair<string, object>>>())
            );
        }
        
        [Theory]
        [InlineData("my-key", "string_value")]
        [InlineData("another.key", 1337)]
        [InlineData("also_booleans_work", false)]
        public void When_with_parameters_Then_include_parameters(string key, object value)
        {
            // Arrange
            _builder.With(key, value);
            
            // Act
            _builder.Begin();
            
            // Assert
            _logger.Verify(
                log => log.BeginScope(
                    It.Is<IReadOnlyCollection<KeyValuePair<string, object>>>(
                        dict => dict.Any(p => p.Key == key && p.Value == value))
                )
            );
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once ClassNeverInstantiated.Global
        public class DummyService { }
    }
}