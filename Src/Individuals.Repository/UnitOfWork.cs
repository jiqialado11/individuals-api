using System.Threading.Tasks;
using Individuals.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Individuals.Persistance
{
    public class UnitOfWork<C> : IUnitOfWork where C : DbContext
    {
        private readonly C _context;

        public UnitOfWork(C context)
        {

            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {

            return _context.SaveChanges();

        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
