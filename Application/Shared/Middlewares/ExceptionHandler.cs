using System;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Shared.Middlewares
{
    public class ExceptionHandler<TRequest> : IRequestExceptionAction<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public ExceptionHandler(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Execute(TRequest request, System.Exception exception, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _logger.LogError(exception, "[Handler][Error] => type: {@requestType} request: {@request}", request.GetType(), request);
        }
    }
}

