using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Individuals.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Individuals.Persistance
{

    public abstract class RepositoryBase<C, T, K> : IRepositoryBase<T, K> where T : class where C : DbContext
    {
        public C Context { get; set; }

        protected RepositoryBase(C context)
        {
            Context = context;
        }


        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            if (orderBy != null) return orderBy(query).ToList();

            return query.ToList();
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int count = 1000)
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);
            return await query.Take(count).ToListAsync();
        }
        public virtual Task<T> FindSingle(K id)
        {
            return Context.Set<T>().FindAsync(id);
        }

        public virtual Task<T> FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);
            return query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual void Add(T toAdd)
        {
            Context.Set<T>().Add(toAdd);
        }
        public virtual async Task AddAsync(T toAdd)
        {
            await Context.Set<T>().AddAsync(toAdd);
        }
        public virtual void Update(T toUpdate)
        {
            Context.Entry(toUpdate).State = EntityState.Modified;


        }

        public virtual async Task Delete(K id)
        {
            var entity = await FindSingle(id);
            Context.Set<T>().Remove(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }


}
