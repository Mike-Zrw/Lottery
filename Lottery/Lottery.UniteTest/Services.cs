using Lottery.Core.DataModel;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using Lottery.Repository;
using Lottery.Service;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.UniteTest
{
    public class Services
    {
        public IBUserService userSer { get; set; }
        public IBDeskUserService deskuserSer { get; set; }
        public Services()
        {
            userSer = IoC.Resolve<IBUserService>();
            deskuserSer = IoC.Resolve<IBDeskUserService>();
        }
        public List<BUser> FindUsers()
        {
            LogHelper.WriteError(typeof(Services), new Exception("123"));
            return userSer.FindBUser(new BUser()).ToList();
        }
        public JsonResult<IEnumerable<BUser>> DeleteUsers()
        {
            BUser[] users = new BUser[] { new BUser() { USE_ID = 4 }, new BUser() { USE_ID = 5 } };
            return userSer.DeleteRange(users);
        }
        public JsonResult<BDeskUserDto> RegistDeskUser()
        {
            return deskuserSer.Register(new BDeskUserDto() { 
   DUE_EMAIL="@", DUE_SUT_ID=1,DUE_SEX=1, DUE_REALNAME="zs", DUE_PHONE="123123", DUE_USERDSPNAME="123", USE_ACTIVITY=true, USE_NAME="123", USE_PASSWORD="123", USE_UGP_ID=1            
            });
        }
        public List<BDeskUserDto> findDescUser()
        {
            return deskuserSer.FindBDescUser(new BDeskUserDto()).ToList();
        }

    }
}
