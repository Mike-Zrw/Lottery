using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IRepository
{
    public interface IDbContext:IDisposable
    {
        DbContext Self { get; }
    }
    public interface ILotteryDbContext : IDbContext
    {

    }
}
