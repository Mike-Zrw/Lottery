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
        private ICrudRepository<BDeskUser> deskRpt1;
        private ICrudRepository<BDeskUser> deskRpt2;
        private ICrudRepository<BDeskUser> deskRpt3;
        private ICrudRepository<BDeskUser> deskRpt4;
        private ICrudRepository<BDeskUser> deskRpt5;
        private ICrudRepository<BDeskUser> deskRpt6;
        private ICrudRepository<BDeskUser> deskRpt7;
        private ICrudRepository<BDeskUser> deskRpt8;
        private ICrudRepository<BDeskUser> deskRpt9;
        private ICrudRepository<BDeskUser> deskRpt10;
        public BUserService(ILotteryRepository repository)
        {
            this.repository = repository;
            userRpt = repository.GetCrudRepository<BUser>();
            deskRpt1 = repository.GetCrudRepository<BDeskUser>();
            deskRpt2 = repository.GetCrudRepository<BDeskUser>();
            deskRpt3 = repository.GetCrudRepository<BDeskUser>();
            deskRpt4 = repository.GetCrudRepository<BDeskUser>();
            deskRpt5 = repository.GetCrudRepository<BDeskUser>();
            deskRpt6 = repository.GetCrudRepository<BDeskUser>();
            deskRpt7 = repository.GetCrudRepository<BDeskUser>();
            deskRpt8 = repository.GetCrudRepository<BDeskUser>();
            deskRpt9 = repository.GetCrudRepository<BDeskUser>();
            deskRpt10 = repository.GetCrudRepository<BDeskUser>();
        }
        public IQueryable<BUser> FindBUser(BUser user)
        {
            return userRpt.GetAll();
        }


        public JsonResult<IEnumerable<BUser>> DeleteRange(BUser[] users)
        {
           int[] ids =  (from d in users select d.USE_ID).ToArray();
           BUser[] deletes = userRpt.Where(m => ids.Contains(m.USE_ID)).ToArray();
           IEnumerable<BUser> results= userRpt.DeleteRange(deletes);
           repository.Save();
           return new JsonResult<IEnumerable<BUser>>(results);
        }


        public List<BUser> FindBUserList(BUser users)
        {
            return userRpt.GetAll().Take(50000).ToList();
        }
    }
}
