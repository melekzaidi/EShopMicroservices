using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace BuildingBlocks.Behaviors
{
    public class LoggingBehaviors<TRequest, TResponse>
        (ILogger<LoggingBehaviors<TRequest, TResponse>> logger) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
    {
       
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Reponse={Response}", typeof(TRequest).Name, typeof(TResponse).Name, request);
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var timeTaken=timer.Elapsed;
            if (timeTaken.Seconds > 2) {
                logger.LogInformation("[Performance] the request {Request} took {TimeTaken}",typeof(TRequest).Name,timeTaken.Seconds);
            }
            logger.LogInformation("[END] the request {Request} with {Response}",
                typeof(TRequest).Name,typeof(TResponse).Name);
            return response;
        }
    }
   
}
