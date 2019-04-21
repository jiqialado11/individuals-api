using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Individuals.Shared.Contracts
{
    public interface IRepositoryBase { }
    public interface IRepositoryBase<T, K> : IRepositoryBase where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int count = 1000);

        Task<T> FindSingle(K id);

        Task<T> FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "");

        void Add(T toAdd);
        Task AddAsync(T toAdd);
        void Update(T toUpdate);

        Task Delete(K id);

        void Delete(T entity);
    }

}
