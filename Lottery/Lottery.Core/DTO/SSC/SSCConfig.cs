using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lottery.Core.DTO.SSC
{
    public class SSCConfigs
    {
        private static List<SSCConfig> listConfig { get; set; }
        public static List<SSCConfig> ListConfig
        {
            get
            {
                if (listConfig == null)
                {
                    Init();
                }
                if (DateTime.Now > listConfig.Max(m => m.EndTime))
                {
                    Init();
                }
                return listConfig;
            }
        }
        /// <summary>
        /// 当前时间段内的配置
        /// </summary>
        public static SSCConfig Config
        {
            get
            {
                DateTime dtnow = DateTime.Now;
                SSCConfig config = ListConfig.Where(m => dtnow >= m.StartTime && dtnow < m.EndTime).FirstOrDefault();
                return config;
            }
        }
        public static void Init()
        {
            listConfig = new List<SSCConfig>();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlDocument xmlDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "bin\\SSCConfig.xml", settings);
            xmlDoc.Load(reader);
            XmlNode xmlNode = xmlDoc.SelectSingleNode("Config");
            foreach (XmlNode item in xmlNode.ChildNodes)
            {
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd ");
                string dateTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd ");
                TimeSpan tsStart = TimeSpan.Parse(item.ChildNodes[0].InnerText);
                TimeSpan tsEnd = TimeSpan.Parse(item.ChildNodes[1].InnerText);
                SSCConfig config = new SSCConfig()
                {
                    StartTime = Convert.ToDateTime(dateNow + item.ChildNodes[0].InnerText),
                    EndTime = Convert.ToDateTime(((tsEnd > tsStart) ? dateNow : dateTomorrow) + item.ChildNodes[1].InnerText),
                    RefreshTime = Convert.ToInt32(item.ChildNodes[2].InnerText),
                    ShowRefreshTime = Convert.ToInt32(item.ChildNodes[3].InnerText),
                    Sum = Convert.ToInt32(item.ChildNodes[4].InnerText),
                    BeLoginDt = Convert.ToDateTime(dateNow + " 00:00:00")
                };
                listConfig.Add(config);
            }
        }
    }
    /// <summary>
    /// 时时彩配置
    /// </summary>
    public class SSCConfig
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 开彩期数
        /// </summary>
        public int Sum { get; set; }
        public int RefreshTime { get; set; }
        public int ShowRefreshTime { get; set; }

        /// <summary>
        /// 属于哪一天
        /// </summary>
        public DateTime BeLoginDt { get; set; }
    }
}
