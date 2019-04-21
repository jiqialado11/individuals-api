using Individuals.Domain.Entities;
using Individuals.Shared.Contracts;

namespace Individuals.Persistance.Repositories.Contracts
{
    public interface IPhoneNumbersRepository:IRepositoryBase<PhoneNumber,long>
    {
    }
}
