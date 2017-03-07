using log4net;
using Lottery.ApiReference;
using Lottery.Core.DTO.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Lottery.Api.Tasks
{
    /// <summary>
    /// 网易短信接口
    /// </summary>
    public class NetEaseSMS
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public HttpClient _httpClient;
        public static readonly string NetEaseSMSUri = ConfigurationManager.AppSettings["NetEaseSMSUri"].ToString();

        /// <summary>
        /// 发送验证码短信，返回验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="templateid"></param>
        /// <returns></returns>
        public AjaxResult<int> SendMsg(string phone, int templateid)
        {
            string appKey = ConfigurationManager.AppSettings["appKey"].ToString();//appkey由网易云信提供  
            string appSecret = ConfigurationManager.AppSettings["appSecret"].ToString();
            string nonce = new Random().Next(1, 128).ToString();

            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            String curTime = ticks.ToString();
            String checkSum = CheckSumBuilder.getCheckSum(appSecret, nonce, curTime);
            string post = "mobile=" + phone + "&templateid=" + templateid;
            string urlEncoded = Uri.EscapeUriString(post.ToString());

            System.Net.WebRequest wReq = System.Net.WebRequest.Create(NetEaseSMSUri + "sms/sendcode.action?" + urlEncoded);
            wReq.Method = "POST";
            wReq.Headers.Add("AppKey", appKey);
            wReq.Headers.Add("Nonce", nonce);
            wReq.Headers.Add("CurTime", curTime);
            wReq.Headers.Add("CheckSum", checkSum);
            wReq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";


            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();

            string apiresult;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.UTF8))
            {
                apiresult = reader.ReadToEnd();
            }
            AjaxResult<int> result = new AjaxResult<int>(Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(apiresult).obj));
            return result;
        }
    }


    class CheckSumBuilder
    {
        // 计算并获取CheckSum  
        public static String getCheckSum(String appSecret, String nonce, String curTime)
        {
            byte[] data = Encoding.Default.GetBytes(appSecret + nonce + curTime);
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.  
            result = sha.ComputeHash(data);

            return getFormattedText(result);
        }

        // 计算并获取md5值  
        public static String getMD5(String requestBody)
        {
            if (requestBody == null)
                return null;

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(requestBody));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data   
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.  
            return getFormattedText(Encoding.Default.GetBytes(sBuilder.ToString()));
        }

        private static String getFormattedText(byte[] bytes)
        {
            char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5',  
        '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }
    }
}