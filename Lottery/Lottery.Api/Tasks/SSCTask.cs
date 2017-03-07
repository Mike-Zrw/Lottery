using log4net;
using Lottery.ApiReference;
using Lottery.Core.DataModel;
using Lottery.Core.IServices;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;

namespace Lottery.Api.Tasks
{
    public class SSCTask
    {
        private IBSSCService _sscs;
        public SSCTask()
        {
            _sscs = IoC.Resolve<IBSSCService>();
        }
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public  BSSC LatestSSC;
        public static readonly int RefreshTime = ConfigHelper.GetConfigInt("SSCRefreshTime");
        public void Init()
        {
            LatestSSC = new BSSC();
            Thread th = new Thread(new ParameterizedThreadStart(RefreshSSCData));
            th.Start();
        }

        /// <summary>
        /// 定时抓取最新数据
        /// </summary>
        /// <param name="param"></param>
        public void RefreshSSCData(object param)
        {
            while (true)
            {
                SSCApiReference ssc = new SSCApiReference();
                BSSC data = ssc.GetSSCData();
                if (data.SSC_NUMBER != LatestSSC.SSC_NUMBER)
                {
                    BSSC addResult = _sscs.AddFromRemote(data);
                    LatestSSC = addResult;
                }
                Thread.Sleep(RefreshTime * 1000);
            }

        }
    }
}