using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public class BUserService : IBUserService
    {

        private ILotteryRepository repository;
        private ICrudRepository<BUser> userRpt;
        public BUserService(ILotteryRepository repository)
        {
            this.repository = repository;
            userRpt = repository.GetCrudRepository<BUser>();
        }
        public IQueryable<BUser> FindBUser(BUser user)
        {
            return userRpt.GetAll();
        }


        public AjaxResult<IEnumerable<BUser>> DeleteRange(BUser[] users)
        {
           int[] ids =  (from d in users select d.USE_ID).ToArray();
           BUser[] deletes = userRpt.Where(m => ids.Contains(m.USE_ID)).ToArray();
           IEnumerable<BUser> results= userRpt.DeleteRange(deletes);
           repository.Save();
           return new AjaxResult<IEnumerable<BUser>>(results);
        }


        public List<BUser> FindBUserList(BUser users)
        {
            return userRpt.GetAll().Take(50000).ToList();
        }
    }
}
