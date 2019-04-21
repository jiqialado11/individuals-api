using System.Threading.Tasks;
using Individuals.Domain.Entities;
using Individuals.Shared.Contracts;

namespace Individuals.Persistance.Repositories.Contracts
{
    public interface IIndividualsRepository:IRepositoryBase<Individual,long>
    {
        Task<ConnectedIndividual> VerifyConnection(long connectedFromId, long connectedToId);
        Task<bool> RemoveConnection(long connectedFromId, long connectedToId);
    }
}
