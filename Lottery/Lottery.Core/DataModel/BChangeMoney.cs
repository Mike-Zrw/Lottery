//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lottery.Core.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class BChangeMoney
    {
        public int CGM_ID { get; set; }
        public int CGM_USE_ID { get; set; }
        public decimal CGM_MONEY { get; set; }
        public string CGM_SOURSE { get; set; }
        public System.DateTime CGM_CREATETIME { get; set; }
        public decimal CGM_BEFOREMONEY { get; set; }
        public decimal CGM_AFTERMONEY { get; set; }
        public string CGM_DESC { get; set; }
    }
}
