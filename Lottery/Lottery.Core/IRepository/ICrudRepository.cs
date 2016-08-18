using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IRepository
{
    /// <summary>
    /// 仓储操作单元，提供增删该查操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICrudRepository<TEntity> where TEntity : class,new()
    {
        TEntity Add(TEntity t);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Delete(TEntity t);
        IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity t);
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);

        void SetDbContext(System.Data.Entity.DbContext _dbContext);
    }
}
