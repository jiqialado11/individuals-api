using System.Threading.Tasks;
using Individuals.DataAccess.Models.GetIndividual;
using Individuals.DataAccess.Models.QueryIndividuals;

namespace Individuals.DataAccess.Contracts
{
    public interface IIndividualsRelatedQueries
    {
        Task<QueryIndividualsResponse> QueryIndividuals(QueryIndividualsRequest request);
        Task<GetIndividualResponse> GetIndividual(GetIndividualRequest request);
    }
}
