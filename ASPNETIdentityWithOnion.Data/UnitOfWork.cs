using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;

namespace ASPNETIdentityWithOnion.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IEntitiesContext _context;
        private bool _disposed;
        private Hashtable repositories;

        public UnitOfWork(IEntitiesContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)repositories[type];
            }
            var repositoryType = typeof(EntityRepository<>);
            repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
            return (IRepository<TEntity>)repositories[type];
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public int Commit()
        {
            return _context.Commit();
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public Task<int> CommitAsync()
        {
            return _context.CommitAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                foreach (IDisposable repository in repositories.Values)
                {
                    repository.Dispose();// dispose all repositries
                }
            }
            _disposed = true;
        }
    }
}
