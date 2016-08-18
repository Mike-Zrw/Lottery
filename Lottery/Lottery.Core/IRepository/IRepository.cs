using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IRepository
{
    /// <summary>
    /// 仓储基类，提供事物，保存，以及直接执行sql的方法
    /// </summary>
    public interface IRepository : IDisposable
    {
        void BeginTransaction(System.Data.IsolationLevel isoLevel = System.Data.IsolationLevel.ReadCommitted);
        void Commit();
        void RollBack();
        void Save();
        IQueryable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters);
        ICrudRepository<TEntity> GetCrudRepository<TEntity>() where TEntity : class, new();
        int ExecuteCommand(string sql, params object[] parameters);
        int ExecuteNonQuery(string sql, params object[] parameters);
    }
}
