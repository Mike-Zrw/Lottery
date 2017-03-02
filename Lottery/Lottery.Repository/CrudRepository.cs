using Lottery.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Repository
{
    /// <summary>
    /// 仓储操作单元，提供增删该查操作
    /// </summary>
    /// <typeparam name="TEntity">操作主体model</typeparam>
    public class CrudRepository<TEntity> : IDisposable,ICrudRepository<TEntity> where TEntity : class,new()
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _DbSet;
        public void SetDbContext(System.Data.Entity.DbContext dbContext)
        {
            _dbContext = dbContext;
            _DbSet = _dbContext.Set<TEntity>();
        }
        public TEntity Add(TEntity t)
        {
            return _DbSet.Add(t);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return _DbSet.AddRange(entities);
        }

        public TEntity Delete(TEntity t)
        {
            return _DbSet.Remove(t);
        }

        public IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities)
        {
            return _DbSet.RemoveRange(entities);
        }

        public TEntity Update(TEntity t)
        {
          TEntity entity= _DbSet.Attach(t);
          _dbContext.Entry(t).State = EntityState.Modified;
          return entity;
        }

        public TEntity Find(params object[] keyValues)
        {
            return _DbSet.Find(keyValues);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _DbSet.FindAsync(keyValues);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _DbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _DbSet.Where(predicate);
        }

        public bool Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _DbSet.Any(predicate);
        }
        #region 关闭连接

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion 关闭连接



        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
