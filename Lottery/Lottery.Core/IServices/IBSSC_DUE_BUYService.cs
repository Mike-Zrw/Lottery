using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IServices
{
    public interface IBSSC_DUE_BUYService
    {
        DTO.Common.AjaxResult<string> Buy(List<DataModel.BSSC_DUE_BUY> lists);
    }
}
