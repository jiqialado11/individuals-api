using Individuals.Shared;
using MediatR;

namespace Individuals.Queries.Individuals.GetIndividual
{
    public class GetIndividualQuery:IRequest<Result>
    {
        public long? Id { get; set; }
    }
}
