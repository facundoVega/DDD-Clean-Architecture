using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CleanArchitecture.Applications.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseRequest
where TResponse: Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try 
        {
            _logger.LogInformation($"Executing the request: { name }.", name);
            var result = await next();

            if (result.IsSuccess) 
            {
                _logger.LogInformation($"The request: { name } was successful.", name);
            }
            else
            {
                using(LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError($"The request { name } has errors", name);
                }
            }

            return result;
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, $"The request { name } had errors.", name);
            throw;
        } 
    }
}