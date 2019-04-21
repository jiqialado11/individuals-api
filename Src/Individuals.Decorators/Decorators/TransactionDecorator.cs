using System;
using System.Threading;
using System.Threading.Tasks;
using Individuals.Shared;
using Individuals.Shared.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Individuals.Decorators.Decorators
{
    public class TransactionDecorator<TRequest,TResult> : IPipelineBehavior<TRequest,TResult>
  where TResult : Result
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TransactionDecorator<TRequest,TResult>> _logger;

        public TransactionDecorator(IUnitOfWork uow, ILogger<TransactionDecorator<TRequest, TResult>> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            TResult handleResult;
            var result = await Policy
                .Handle((DbUpdateException ex) =>
                {
                    _logger.LogError($"TransactionDecorator DbUpdateException. ", ex.Message);
                    _uow.Dispose();
                    return true;

                })
                .Or((ObjectDisposedException ex) =>
                {
                    _logger.LogError($"TransactionDecorator ObjectDisposedException. ", ex.Message);
                    _uow.Dispose();
                    return true;
                })
                .RetryAsync(0)
                .ExecuteAndCaptureAsync(async () =>
                {
                     handleResult = await next();
                    if (!handleResult.IsSuccess )
                    {
                        _uow.Dispose();
                        return handleResult;

                    }
                     await _uow.SaveAsync();
                     return handleResult;
                    
                });
           
            return result.Outcome == OutcomeType.Successful ? result.Result :
                Result.Error(ResultType.InternalServerError,result.FinalException.Message) as TResult;

        }


       
    }

  
}
