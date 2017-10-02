using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Data
{
    class ApplicationDbContext : DbContext, IEntitiesContext
    {
        private ObjectContext objectContext;
        private DbTransaction transaction;
        private static readonly object Lock = new object();
        private static bool databaseInitialized;

        public ApplicationDbContext(string nameOrConnectionString, ILogger logger) : base(nameOrConnectionString)
        {
            Database.Log = logger.Log;
            if (databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!databaseInitialized)
                {
                    // Set the database intializer which is run once during application start
                    // This seeds the database with admin user credentials and admin role
                    Database.SetInitializer(new ApplicationDbInitializer());
                    databaseInitialized = true;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EfConfig.ConfigureEf(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction()
        {
            objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }
            objectContext.Connection.Open();
            transaction = objectContext.Connection.BeginTransaction();
        }

        public int Commit()
        {
            var saveChanges = SaveChanges();
            transaction.Commit();
            return saveChanges;
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            transaction.Commit();
            return saveChangesAsync;
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (objectContext != null && objectContext.Connection.State == ConnectionState.Open)
                {
                    objectContext.Connection.Close();
                }
                if (objectContext != null)
                {
                    objectContext.Dispose();
                }
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }

}
