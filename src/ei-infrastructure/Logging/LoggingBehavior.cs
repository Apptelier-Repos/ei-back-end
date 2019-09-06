using System.Threading;
using System.Threading.Tasks;
using ei_core.Interfaces;
using MediatR;

namespace ei_infrastructure.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAppLogger<TRequest> _logger;

        public LoggingBehavior(IAppLogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation(request, "Calling handler...");
            var response = await next();
            _logger.LogInformation(request, "Called handler with result {0}", response);
            return response;
        }
    }
}