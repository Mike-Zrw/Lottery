using Lottery.Core.DataModel;
using Lottery.Core.IServices;
using Lottery.Tools;
using Lottery.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IBUserService us1;
        private IBUserService us2;
        private IBUserService us3;
        private IBUserService us4;
        private IBUserService us5;
        private IBUserService us6;
        private IBUserService us;
        public HomeController(IBUserService userservice, IBUserService userservice1, IBUserService userservice2, IBUserService userservice3, IBUserService userservice4,
            IBUserService userservice5, IBUserService userservice6)
        {
            this.us = userservice;
            this.us1 = userservice1;
            this.us2 = userservice2;
            this.us3 = userservice3;
            this.us4 = userservice4;
            this.us5 = userservice5;
            this.us6 = userservice6;
        }
        public ActionResult Index()
        {
            List<BUser> list = us.FindBUserList(new BUser());
            return View();
        }

    }
}
