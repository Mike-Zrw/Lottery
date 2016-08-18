﻿using Lottery.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Repository
{
    /// <summary>
    /// lotter仓储基类
    /// </summary>
    public class LotteryRepository :EfRepository, ILotteryRepository
    {
        private ILotteryDbContext DbContext;
        public LotteryRepository(ILotteryDbContext DbContext):base(DbContext)
        {
            this.DbContext = DbContext;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
