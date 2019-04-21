using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Individuals.Shared;
using MediatR;

namespace Individuals.Decorators.Decorators
{
    public class ValidationDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TResult:Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationDecorator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var dictionary = new Dictionary<string, string>();

                foreach (var failure in failures)
                {
                    dictionary.Add(failure.PropertyName, failure.ErrorMessage);

                }

                return Task.FromResult(Result.Error(ResultType.UnsupportedMediaType,"Validation exception",null, dictionary)).Result as TResult;
            }

            return await next();
        }


       
    }

}
