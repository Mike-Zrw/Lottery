using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IServices
{
    public interface IMoneyService
    {
        DTO.Common.AjaxResult<string> AddMoney(DataModel.PayOnLine pol);
    }
}
