using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.DTO.SSC;
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
        AjaxResult<NextSSC> GetNextSSC();

        PageSplit<List<BSSC>> GetBSSC(int start, int limit);

        PageSplit<List<BSSC>> GetNewSSC(int maxid);
        /// <summary>
        /// 填充空数据，只在首次启动程序调用
        /// </summary>
        void CheckNullData();

        List<RSSC_TYPE> GetSSC_TYPE();
    }
}
