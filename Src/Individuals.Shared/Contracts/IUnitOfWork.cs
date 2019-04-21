using System;
using System.Threading.Tasks;

namespace Individuals.Shared.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        int Save();
        Task<int> SaveAsync();
    }
}
