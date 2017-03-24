using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.DTO
{
    public class BDeskUserDto
    {
        public int DUE_ID { get; set; }
        public int DUE_USE_ID { get; set; }
        public System.DateTime DUE_REGISTTIME { get; set; }
        public string DUE_REALNAME { get; set; }
        public string DUE_PHONE { get; set; }
        public string DUE_USERDSPNAME { get; set; }
        public Nullable<int> DUE_SEX { get; set; }
        public string DUE_EMAIL { get; set; }
        public int DUE_SUT_ID { get; set; }
        public int USE_ID { get; set; }
        public string USE_NAME { get; set; }
        public string USE_PASSWORD { get; set; }
        public bool USE_ACTIVITY { get; set; }
        public int USE_UGP_ID { get; set; }
        public string DUE_WX_TOKEN { get; set; }
        public string DUE_QQ_TOKEN { get; set; }
        public string DUE_WB_TOKEN { get; set; }
    }
}
