using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.Services;


namespace ASPNETIdentityWithOnion.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private readonly IRepository<TEntity> repository;
        private bool disposed;

        public Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            repository = UnitOfWork.Repository<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return repository.GetAll();
            
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return repository.GetAll(pageIndex, pageSize);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return repository.GetAll(pageIndex, pageSize, keySelector, orderBy);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return repository.GetAll(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties);
        }

        public TEntity GetById(int id)
        {
            return repository.GetSingle(id);
        }

        public void Add(TEntity entity)
        {
            repository.Insert(entity);
            UnitOfWork.SaveChanges();
        }
        
        public void Update(TEntity entity)
        {
            repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
            UnitOfWork.SaveChanges();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize)
        {
            return repository.GetAllAsync(pageIndex, pageSize);
        }

        public Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return repository.GetAllAsync(pageIndex, pageSize, keySelector, orderBy);
        }

        public Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return repository.GetAllAsync(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties);
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return repository.GetSingleAsync(id);
        }

        public Task AddAsync(TEntity entity)
        {
            repository.Insert(entity);
            return UnitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            repository.Update(entity);
            return UnitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(TEntity entity)
        {
            repository.Delete(entity);
            return UnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                UnitOfWork.Dispose();
            }
            disposed = true;
        }
    }
}
