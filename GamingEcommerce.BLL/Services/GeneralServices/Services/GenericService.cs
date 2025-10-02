using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using System.Linq.Expressions;
using GamingEcommerce.DAL.DataContext.Contracts;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class GenericService<TEntity, TCreateViewModel, TUpdateViewModel, TViewModel> : IGenericService<TEntity, TCreateViewModel, TUpdateViewModel, TViewModel> where TEntity : BaseEntity
    {
        internal readonly IRepository<TEntity> Repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public async Task<TViewModel> AddAsync(TCreateViewModel entity)
        {
            var mappedEntity = _mapper.Map<TEntity>(entity);
            await Repository.AddAsync(mappedEntity);

            return _mapper.Map<TViewModel>(mappedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            
            if(entity == null)
            {
                return false;
            }

            return await Repository.DeleteAsync(entity);
        }

        public virtual async Task<List<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool asnotracking = false)
        {
            var listFromDb = await Repository.GetAllAsync(predicate, include, orderBy, asnotracking);

            var list = _mapper.Map<List<TViewModel>>(listFromDb);

            return list.ToList();

        }

        public async Task<TViewModel?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool asnotracking = false)
        {
            var entityFromDb = await Repository.GetAsync(predicate, include, asnotracking);

            if (entityFromDb == null)
            {
                return default;
            }

            var entity = _mapper.Map<TViewModel>(entityFromDb);

            return entity;
        }

        public virtual async Task<TViewModel?> GetByIdAsync(int id)
        {
            var entityFromDb = await Repository.GetByIdAsync(id);

            if (entityFromDb == null)
            {
                return default;
            }

            var entity = _mapper.Map<TViewModel>(entityFromDb);

            return entity;
        }

        public async Task<bool> UpdateAsync(TUpdateViewModel entity)
        {
            var mappedEntity = _mapper.Map<TEntity>(entity);

            if(mappedEntity.Id == 0)
            {
                return false;
            }

            return await Repository.UpdateAsync(mappedEntity);
        }
    }
}
