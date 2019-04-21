using Individuals.Domain.Entities;
using Individuals.Persistance.Repositories.Contracts;

namespace Individuals.Persistance.Repositories
{
    public class PhoneNumbersRepository:RepositoryBase<IndividualsDBContext,PhoneNumber,long>,IPhoneNumbersRepository
    {
        public PhoneNumbersRepository(IndividualsDBContext context) : base(context)
        {
        }
    }
}
