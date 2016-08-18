using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery.Web
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
        }
    }
}