using Microsoft.EntityFrameworkCore.Query;
using GamingEcommerce.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface IGenericService<TEntity, TCreateViewModel, TUpdateViewModel, TViewModel> where TEntity : BaseEntity
    {
        Task<TViewModel?> GetByIdAsync(int id);

        Task<TViewModel?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool asnotracking = false);

        Task<List<TViewModel>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool asnotracking = false);
            
        Task<TViewModel> AddAsync(TCreateViewModel entity);

        Task<bool> UpdateAsync(TUpdateViewModel entity);

        Task<bool> DeleteAsync(int id);
    }
}
