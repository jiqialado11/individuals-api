using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Individuals.DataAccess.Contracts;
using Individuals.DataAccess.Models.QueryIndividuals;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Shared;
using MediatR;

namespace Individuals.Queries.Individuals.QueryIndividuals
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(CachingDecorator<,>))]
    [BaseDecorator(typeof(QueryRetryDecorator<,>))]
    public class QueryIndividualsQueryHandler:IRequestHandler<QueryIndividualsQuery,Result>
    {
        private readonly IIndividualsRelatedQueries _individualsRelatedQueries;
        public QueryIndividualsQueryHandler(
            IIndividualsRelatedQueries individualsRelatedQueries)
        {
            _individualsRelatedQueries = individualsRelatedQueries;
        }
        
        public async Task<Result> Handle(QueryIndividualsQuery request, CancellationToken cancellationToken)
        {
            var result =
                await _individualsRelatedQueries.QueryIndividuals(
                    Mapper.Map<QueryIndividualsQuery, QueryIndividualsRequest>(request));

            if (result.List.Any())
                return Result.Ok(result);
            
            return Result.Error(ResultType.NotFound,
                "Could not find individuals with provided parameters");
        }
    }
}
