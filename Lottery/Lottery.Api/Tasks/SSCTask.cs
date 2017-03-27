using log4net;
using Lottery.ApiReference;
using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.DTO.SSC;
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
    /// <summary>
    /// 时时彩网络请求
    /// </summary>
    public class SSCTask
    {
        private IBSSCService _sscs;
        public SSCTask()
        {
            _sscs = IoC.Resolve<IBSSCService>();
        }
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BSSC LatestSSC;
        /// <summary>
        /// 初始化开启线程抓数据
        /// </summary>
        public void Init()
        {
            _sscs.CheckNullData();
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
                SSCConfig config = SSCConfigs.Config;
                if (config == null)  //若当前时间段内没有配置，说明当天结束，等待新一期的开奖数据
                {
                    Thread.Sleep(60 * 1000);
                    return;
                }
                int RefreshTime = config.RefreshTime;
                SSCApiReference sscApi = new SSCApiReference();
                BSSC data = sscApi.GetSSCData();
                if (data == null)//接口错误
                {
                    //需要伪造一组数据来插入下一期的数据中
                    AjaxResult<BSSC> nextssc = _sscs.GetNextSSC();
                    if (nextssc.Success)  
                    {
                        nextssc.Data.SSC_NO = string.Join(",", new Random(10000).Next(99999).ToString().ToCharArray());
                        nextssc.Data.SSC_WRITEDT = DateTime.Now;
                        BSSC addResult = _sscs.AddFromRemote(nextssc.Data);
                        LatestSSC = addResult;
                    }
                    Thread.Sleep(RefreshTime * 1000);
                    return;
                }
                if (string.IsNullOrWhiteSpace(data.SSC_NO))//未取到数据，接口并没有开奖数据
                {
                    Thread.Sleep(RefreshTime * 1000);
                    return;
                }
                if (data.SSC_NUMBER != LatestSSC.SSC_NUMBER)
                {
                    BSSC addResult = _sscs.AddFromRemote(data);
                    LatestSSC = addResult;
                    Thread.Sleep(RefreshTime * 1000);
                }
            }

        }
    }
}