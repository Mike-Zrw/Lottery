using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public class BSSC_DUE_BUYService : IBSSC_DUE_BUYService
    {
        public AjaxResult<string> Buy(List<BSSC_DUE_BUY> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                lists[i].SCD_DATE = DateTime.Now;
                lists[i].SCD_TIMES = 0;
            }
            return null;
        }
    }
}
