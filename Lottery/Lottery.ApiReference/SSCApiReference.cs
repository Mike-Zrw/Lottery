using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Lottery.Core.DTO.SSC;
using Lottery.Core.DataModel;
using System.Xml;

namespace Lottery.ApiReference
{
    public class SSCApiReference : Base_SSCApiReference
    {
        /// <summary>
        /// 获取最新一期数据
        /// </summary>
        /// <returns>没有数据返回空对象，接口错误返回null</returns>
        public BSSC GetSSCData()
        {
            try
            {
                using (HttpResponseMessage response = HttpClient.GetAsync("static/public/ssc/xml/qihaoxml/" + DateTime.Now.ToString("yyyyMMdd") + ".xml?_A=" + new Guid().ToString()).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(result);
                        XmlNode root = xmlDoc.SelectSingleNode("xml");
                        if (root.ChildNodes.Count > 0)
                        {

                            XmlNode node = root.FirstChild;
                            BSSC ssc = new BSSC()
                            {
                                SSC_NO = node.Attributes["expect"].Value,
                                SSC_NUMBER = node.Attributes["opencode"].Value
                            };
                            return ssc;
                        }
                        else
                            return new BSSC();

                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}
