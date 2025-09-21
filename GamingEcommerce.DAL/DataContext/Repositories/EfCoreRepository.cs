using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext DbContext;

        public EfCoreRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            var existingEntity = await DbContext.Set<T>().FindAsync(entity.Id);

            if (existingEntity != null)
            {
                DbContext.Set<T>().Remove(existingEntity);
                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;

        }

        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool asnotracking = false)
        {
            var query = DbContext.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (asnotracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool asnotracking = false)
        {
            var query = DbContext.Set<T>().AsQueryable();

            query = query.Where(predicate);
            
            if (include != null)
            {
                query = include(query);
            }

            if (asnotracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var existingEntity = await DbContext.Set<T>().FindAsync(entity.Id);

            if (existingEntity != null)
            {
                DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
