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
    public class BuyController : ApiController
    {
        private IBSSC_DUE_BUYService _sdb;
        public BuyController(IBSSC_DUE_BUYService sdb)
        {
            _sdb = sdb;
        }
        public AjaxResult<string> Buy(List<BSSC_DUE_BUY> lists)
        {
            return _sdb.Buy(lists);
        }
    }
}
