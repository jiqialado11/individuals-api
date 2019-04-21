using Individuals.DataAccess.Models.GetIndividual.Response_Objects;

namespace Individuals.DataAccess.Models.GetIndividual
{
    public class GetIndividualRequest
    {
        public long? Id { get; set; }
    }
    public class GetIndividualResponse
    {
        public GetIndividualItem Individual { get; set; }
    }
}
