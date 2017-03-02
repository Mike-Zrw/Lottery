using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lottery.Api.Controllers
{
    public class SSCController : ApiController
    {
        private IBSSCService _ssc;
        public SSCController(IBSSCService ssc)
        {
            _ssc = ssc;
        }
        /// <summary>
        /// 查询时时开奖数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult<List<BSSC>> GetBSSC(string date, string no)
        {
            return _ssc.GetBSSC(new BSSC() { SSC_DATE = Convert.ToDateTime(date), SSC_NO = no });
        }
    }
}
