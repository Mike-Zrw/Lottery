using Lottery.ApiReference;
using Lottery.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Api.Tasks
{
    public class Tasks
    {
        public static void Init()
        {
            SSCTask st = new SSCTask();
            st.Init();
        }
    }
}