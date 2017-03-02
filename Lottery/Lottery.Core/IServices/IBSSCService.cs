using Lottery.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IServices
{
   public interface IBSSCService
    {
        DataModel.BSSC AddFromRemote(DataModel.BSSC data);

        DTO.Common.AjaxResult<List<BSSC>> GetBSSC(DataModel.BSSC bSSC);
    }
}
