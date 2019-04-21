using Individuals.Domain.Enums;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.SetConnectedIndividual
{
    public class SetConnectedIndividualCommand:IRequest<Result>
    {
        public long? ConnectedFromIndividualId { get; set; }
        public long? ConnectedToIndividualId { get; set; }
        public IndividualsConnectionType? ConnectionType { get; set; }
    }
}
