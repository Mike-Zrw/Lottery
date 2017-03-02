using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.DTO.Common
{
    /// <summary>
    /// 公共执行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AjaxResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        public AjaxResult(bool success,string error,T data)
        {
            this.Success = success;
            this.Error = error;
            this.Data = data;
        }
        public AjaxResult(T data)
        {
            this.Success = true;
            this.Data = data;
        }

        public AjaxResult(bool success, string error)
        {
            this.Success = success;
            this.Error = error;
        }
    }
}
