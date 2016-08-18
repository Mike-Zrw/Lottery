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
        JsonResult<BDeskUserDto> Login(string name, string pass);
        JsonResult<BDeskUserDto> Register(BDeskUserDto dto);
        IQueryable<BDeskUserDto> FindBDescUser(BDeskUserDto dto);
    }
}
