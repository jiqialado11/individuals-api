using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Individuals.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace Individuals.Decorators.Decorators
{
    public class QueryRetryDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TResult : Result
    {
        private readonly ILogger<CachingDecorator<TRequest, TResult>> _logger;

        public QueryRetryDecorator(ILogger<CachingDecorator<TRequest, TResult>> logger)
        {
            _logger = logger;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            var typo = typeof(TResult).GetGenericArguments().FirstOrDefault();
         
            var result = await Policy
                    .Handle((SqlException ex) =>
                    {
                        _logger.LogError("QueryRetryDecorator Exception.", ex.Message);
                        return true;
                    })
                    .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                    }, (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogInformation($"QueryRetryDecorator Retry for Exception:{exception.Message}, Count:{retryCount}.");
                    })
                    .ExecuteAndCaptureAsync(async () => await next());

            if (result.Outcome == OutcomeType.Successful)
                return result.Result;

            return
                Result.Error(ResultType.InternalServerError, result.FinalException.Message, result.FinalException) as TResult; 
    
        }


      
    }

}
