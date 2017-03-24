using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Core.DTO.Common
{
    public class PageSplit<T>
    {
        public PageSplit()
        {

        }
        public PageSplit(T items, int total, int start, int limit)
        {
            this.Total = total;
            this.Items = items;
            this.Start = start;
            this.Limit = limit;
        }
        public int Total { get; set; }
        public T Items { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
    }
}
