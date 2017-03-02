using Lottery.Core.DataModel;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IServices
{
    public interface IBDeskUserService
    {
        AjaxResult<BDeskUserDto> Login(string name, string pass);
        AjaxResult<BDeskUserDto> Register(BDeskUserDto dto);
        IQueryable<BDeskUserDto> FindBDeskUser(BDeskUserDto dto);
    }
}
