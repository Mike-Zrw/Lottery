using Lottery.Core.IRepository;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Repository
{
    /// <summary>
    /// entityFramework仓储基类
    /// </summary>
    public class EfRepository : IRepository
    {
        protected readonly DbContext _dbContext;
        public EfRepository(IDbContext dbcontext)
        {
            _dbContext = dbcontext.Self;
            int timeout = ConfigHelper.GetConfigInt("EntityFrameworkConnectTimeout");
            _dbContext.Database.CommandTimeout = timeout == 0 ? 300 : timeout;
        }
        #region 事物处理
        private DbContextTransaction _transaction;
        public void BeginTransaction(System.Data.IsolationLevel isoLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            if (_transaction != null)
            {
                throw new Exception("存在未提交的事务，不能开始事务,请检查编码错误");
            }
            try
            {
                _transaction = _dbContext.Database.BeginTransaction(isoLevel);
            }
            catch (Exception ex)
            {
                _transaction = null;
                throw ex;
            }
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new Exception("事务不存在，请检查编码错误");
            }
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _transaction = null;
            }
        }

        public void RollBack()
        {
            if (_transaction == null)
            {
                throw new Exception("事务不存在，请检查编码错误");
            }
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _transaction = null;
            }
        }
        #endregion
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery<TEntity>(sql, parameters).AsQueryable<TEntity>();
        }

        public ICrudRepository<TEntity> GetCrudRepository<TEntity>() where TEntity : class, new()
        {
            ICrudRepository<TEntity> crud = IoC.Resolve<ICrudRepository<TEntity>>(); ; 
            crud.SetDbContext(_dbContext);
            return crud;
        }
        public int ExecuteCommand(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, parameters);
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

    }
}
