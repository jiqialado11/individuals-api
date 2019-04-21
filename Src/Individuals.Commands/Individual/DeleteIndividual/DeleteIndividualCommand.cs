using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.DeleteIndividual
{
    public class DeleteIndividualCommand:IRequest<Result>
    {
        public long? Id { get; set; }
    }
}
