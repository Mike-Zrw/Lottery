using Lottery.ApiReference;
using Lottery.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Api.Tasks
{
    /// <summary>
    /// 总任务控制中心
    /// </summary>
    public class Tasks
    {
        public static void Init()
        {
            SSCTask st = new SSCTask();
            st.Init();
        }
    }
}