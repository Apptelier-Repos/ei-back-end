using ei_core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ei_infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(T state, string message, params object[] args)
        {
            using (_logger.BeginScope(state))
            {
                _logger.LogWarning(message, args);
            }
        }

        public void LogInformation(T state, string message, params object[] args)
        {
            using (_logger.BeginScope(state))
            {
                _logger.LogInformation(message, args);
            }
        }
    }
}