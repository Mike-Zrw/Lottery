using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.IServices
{
    public interface IBUserService
    {
        IQueryable<BUser> FindBUser(BUser users);
        List<BUser> FindBUserList(BUser users);
        AjaxResult<IEnumerable<BUser>> DeleteRange(BUser[] users);
    }
}
