using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lottery.Api.Controllers
{
    /// <summary>
    /// 购彩操作
    /// </summary>
    public class BuyController : ApiController
    {
        private IBSSC_DUE_BUYService _sdb;
        public BuyController(IBSSC_DUE_BUYService sdb)
        {
            _sdb = sdb;
        }
        [HttpPost]
        public AjaxResult<string> Buy([FromBody]string Params)
        {
            dynamic ParamObj = JsonConvert.DeserializeObject(Params);
            List<BSSC_DUE_BUY> lists = JsonConvert.DeserializeObject(ParamObj.list);
            string SSC_NO = JsonConvert.DeserializeObject(ParamObj.SSC_NO);
            return _sdb.Buy(lists, SSC_NO);
        }
    }
}
