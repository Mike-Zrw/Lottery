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
    /// 资金操作
    /// </summary>
    public class MoneyController : ApiController
    {
        private IMoneyService _money;
        public MoneyController(IMoneyService money)
        {
            _money = money;
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<string> AddMoney([FromBody]string Params)
        {
            dynamic ParamObj = JsonConvert.DeserializeObject(Params);
            int USE_ID = ParamObj.USE_ID;
            decimal money = ParamObj.Money;
            int DAT_ID = ParamObj.DAT_ID;
            PayOnLine pol = new PayOnLine()
            {
                POL_USE_ID = USE_ID,
                POL_CREATETIME = DateTime.Now,
                POL_CONFIRMTIME = DateTime.Now,
                POL_DAT_ID = DAT_ID,
                POL_MONEY = money,
                POL_STATE = 1
            };
            return _money.AddMoney(pol);
        }

    }
}
