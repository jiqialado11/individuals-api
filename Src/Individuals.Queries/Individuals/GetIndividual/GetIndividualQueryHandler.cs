using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Individuals.DataAccess.Contracts;
using Individuals.DataAccess.Models.GetIndividual;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Shared;
using MediatR;

namespace Individuals.Queries.Individuals.GetIndividual
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(CachingDecorator<,>))]
    [BaseDecorator(typeof(QueryRetryDecorator<,>))]
    public class GetIndividualQueryHandler:IRequestHandler<GetIndividualQuery,Result>
    {
        private readonly IIndividualsRelatedQueries _individualsRelatedQueries;

        public GetIndividualQueryHandler(IIndividualsRelatedQueries individualsRelatedQueries)
        {
            _individualsRelatedQueries = individualsRelatedQueries;
        }

        public async Task<Result> Handle(GetIndividualQuery request, CancellationToken cancellationToken)
        {
            var result =
                await _individualsRelatedQueries.GetIndividual(
                    Mapper.Map<GetIndividualQuery, GetIndividualRequest>(request));

            if (result.Individual!=null)
                return Result.Ok(result);

            return Result.Error(ResultType.NotFound,
                "Could not find individual with provided Identifier");
        }
    }
}
