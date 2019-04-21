using System.Linq;
using System.Threading.Tasks;
using Individuals.Domain.Entities;
using Individuals.Persistance.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Individuals.Persistance.Repositories
{
    public class IndividualsRepository:RepositoryBase<IndividualsDBContext,Individual,long>,IIndividualsRepository
    {
        public IndividualsRepository(IndividualsDBContext context) : base(context)
        {
        }

        public async Task<ConnectedIndividual> VerifyConnection(long connectedFromId, long connectedToId)
        {

            var connection = await Context.ConnectedIndividuals.Where(x =>
                (x.ConnectedFromIndividual.Id == connectedFromId ||
                 x.ConnectedFromIndividual.Id == connectedFromId) 
                &&
                (x.ConnectedToIndividual.Id == connectedToId ||
                 x.ConnectedToIndividual.Id == connectedToId)).FirstOrDefaultAsync();

            return connection;
        }

        public async Task<bool> RemoveConnection(long connectedFromId, long connectedToId)
        {
            var connection = await Context.ConnectedIndividuals.Where(x =>
                (x.ConnectedFromIndividual.Id == connectedFromId ||
                 x.ConnectedFromIndividual.Id == connectedFromId)
                &&
                (x.ConnectedToIndividual.Id == connectedToId ||
                 x.ConnectedToIndividual.Id == connectedToId)).FirstOrDefaultAsync();
            Context.ConnectedIndividuals.Remove(connection);

            return true;
        }
    }
}
