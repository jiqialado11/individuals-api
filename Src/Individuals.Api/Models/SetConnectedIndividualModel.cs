using Individuals.Domain.Enums;

namespace Individuals.Api.Models
{
    public class SetConnectedIndividualModel
    {
        public long? ConnectedFromIndividualId { get; set; }
        public long? ConnectedToIndividualId { get; set; }
        public IndividualsConnectionType? ConnectionType { get; set; }
    }
}
