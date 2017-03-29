using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.DTO.SSC;
using Lottery.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lottery.Api.Controllers
{
    /// <summary>
    /// 彩票操作
    /// </summary>
    public class SSCController : ApiController
    {
        private IBSSCService _ssc;
        public SSCController(IBSSCService ssc)
        {
            _ssc = ssc;
        }
        /// <summary>
        /// 查询玩法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<RSSC_TYPE> GetSSC_TYPE()
        {
            return _ssc.GetSSC_TYPE();
        }
        /// <summary>
        /// 查询时时开奖数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult<List<BSSC>> GetBSSC(string date, string no=null)
        {
            return _ssc.GetBSSC(new BSSC() { SSC_DATE = Convert.ToDateTime(date), SSC_NO = no });
        }
        /// <summary>
        /// 获取今天开奖数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult<List<BSSC>> GetTodaySSC()
        {
            return _ssc.GetBSSC(new BSSC() { SSC_DATE = Convert.ToDateTime(DateTime.Now.ToShortDateString()) });
        } 
        /// <summary>
        /// 获取开奖数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PageSplit<List<BSSC>> GetBSSCSplit(int start,int limit)
        {
            return _ssc.GetBSSC(start,limit);
        } 
        /// <summary>
        /// 界面刷新，获取最新的数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PageSplit<List<BSSC>> GetNewSSC(int maxid)
        {
            return _ssc.GetNewSSC(maxid);
        }
        /// <summary>
        /// 即将开奖数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult<NextSSC> GetNextSSC()
        {
            return _ssc.GetNextSSC();
        }
    }
}
