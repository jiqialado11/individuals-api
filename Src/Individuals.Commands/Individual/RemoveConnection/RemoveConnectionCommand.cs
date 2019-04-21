using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.RemoveConnection
{
    public class RemoveConnectionCommand:IRequest<Result>
    {
        public long? ConnectedFromIndividualId { get; set; }
        public long? ConnectedToIndividualId { get; set; }
    }
}
