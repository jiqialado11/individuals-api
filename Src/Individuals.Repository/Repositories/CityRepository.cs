using Individuals.Domain.Entities;
using Individuals.Persistance.Repositories.Contracts;

namespace Individuals.Persistance.Repositories
{
    public class CityRepository:RepositoryBase<IndividualsDBContext,City,long>,ICityRepository
    {
        public CityRepository(IndividualsDBContext context) : base(context)
        {
        }
    }
}
