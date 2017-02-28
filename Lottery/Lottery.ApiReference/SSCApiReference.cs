using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Lottery.Core.DTO.SSC;

namespace Lottery.ApiReference
{
    public class SSCApiReference : Base_SSCApiReference
    {
        public string  GetSSCData()
        {
            using (HttpResponseMessage response = HttpClient.GetAsync("data/cqssc.json").Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    SSCResult result = response.Content.ReadAsAsync<SSCResult>().Result;
                    return "";
                }
            }
            return null;
        }
    }
}
