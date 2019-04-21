using System.Threading.Tasks;
using Individuals.Application.Models;

namespace Individuals.Application.Contracts
{
    public interface IIndividualsService
    {
        Task<QueryIndividualsResponse> QueryIndividuals(QueryIndividualsRequest request);
    }
}
