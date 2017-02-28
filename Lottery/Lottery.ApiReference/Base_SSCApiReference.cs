using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.ApiReference
{
    public class Base_SSCApiReference : BaseApiReference
    {
        public static readonly string SSCApiUri = ConfigurationManager.AppSettings["SSCApiUri"].ToString();
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Base_SSCApiReference(): base(SSCApiUri)
        {

        }
        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    lock (_syncRoot)
                    {
                        HttpClientHandler httpClientHandler;
                        HttpClient httpClient;
                        InitHttpClient(out httpClientHandler, out httpClient);
                        if (_httpClient == null)
                        {
                            _httpClientHandler = httpClientHandler;
                            _httpClient = httpClient;
                        }
                    }
                }
                return _httpClient;
            }
        }
    }
}
