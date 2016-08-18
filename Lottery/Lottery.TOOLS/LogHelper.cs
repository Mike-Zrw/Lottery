using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "../../log.config", Watch = true)]
namespace Lottery.Tools
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region 
        public static void WriteError(Type t, Exception ex)
        {
            string a = System.AppDomain.CurrentDomain.BaseDirectory;
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region 
        public static void WriteError(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        #endregion


    }
}
