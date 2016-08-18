using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery.Web.Controllers
{

    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult MasterLogin()
        {
            return View();
        }

    }
}
