using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.DTO.SSC
{
    public class SSCResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public SSCResult_data data { get; set; }
    }
    public class SSCResult_data
    {
        public int rows { get; set; }
        public List<SSCResult_data_data> data { get; set; }
    }
    public class SSCResult_data_data
    {
        public int phasetype { get; set; }
        public string phase { get; set; }
        public DateTime time_draw { get; set; }
        public SSC_Result result { get; set; }
    }
    public class SSC_Result
    {
        public string key { get; set; }
        public int[] data { get; set; }
    }

}
