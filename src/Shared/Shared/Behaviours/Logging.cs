
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviours
{
    public class Logging<TRequest, TResponse>(ILogger<Logging<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse :notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {RequestName} with {@Request}", typeof(TRequest).Name, request);

            //the next handler in the pipeline
            var response = await next();

            logger.LogInformation("Handled {RequestName} with {@Response}", typeof(TRequest).Name, response);

            return response;
        }
    }
}
