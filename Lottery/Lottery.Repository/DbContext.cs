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
    /// LotterContext扩展
    /// </summary>
    public partial class LotteryContext : ILotteryDbContext
    {
        public DbContext Self
        {
            get { return this; }
        }
    }

}
