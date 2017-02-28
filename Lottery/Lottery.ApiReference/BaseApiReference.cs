using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.ApiReference
{
    public class BaseApiReference
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public HttpClientHandler _httpClientHandler;
        public HttpClient _httpClient;
        public string _apiUrl;
        /// <summary>
        /// 传入api的站点路径和token的请求地址
        /// </summary>
        /// <param name="apiUrl">网址</param>
        /// <param name="tokenUrl">token的请求地址</param>
        public BaseApiReference(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public static object _syncRoot = new object();
        public HttpClientHandler HttpClientHandler
        {
            get
            {
                if (_httpClientHandler == null)
                {
                    lock (_syncRoot)
                    {
                        HttpClientHandler httpClientHandler;
                        HttpClient httpClient;
                        InitHttpClient(out httpClientHandler, out httpClient);
                        if (_httpClientHandler == null)
                        {
                            _httpClientHandler = httpClientHandler;
                            _httpClient = httpClient;
                        }
                    }
                }
                return _httpClientHandler;
            }
        }

        public void InitHttpClient(out HttpClientHandler httpClientHandler, out HttpClient httpClient)
        {
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseCookies = true;
            httpClient = new HttpClient(httpClientHandler);
            httpClient.BaseAddress = new Uri(_apiUrl);
            httpClient.Timeout = TimeSpan.FromMinutes(30);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
