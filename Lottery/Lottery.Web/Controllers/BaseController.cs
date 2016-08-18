using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lottery.Web.Controllers
{
    public class BaseController:Controller
    {
        public BaseController() { }
        protected override void Dispose(Boolean dispose)
        {
            base.Dispose(dispose);
        }
    }
}
