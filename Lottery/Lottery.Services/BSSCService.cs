using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
using Lottery.Core.DTO.SSC;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public class BSSCService : IBSSCService
    {
        private ILotteryRepository _repository;
        private ICrudRepository<BSSC> _ssc;
        public BSSCService(ILotteryRepository repository)
        {
            _repository = repository;
            _ssc = repository.GetCrudRepository<BSSC>();
        }
        /// <summary>
        /// 增加一条记录，如果没有当太难的记录，那么初始化当天全部记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BSSC AddFromRemote(BSSC data)
        {
            BSSC ssc = _ssc.Where(m => m.SSC_NO == data.SSC_NO).FirstOrDefault();
            if (ssc == null || ssc.SSC_ID == 0)  //新增操作
            {
                int num = Convert.ToInt32(data.SSC_NO.Substring(8));
                Random rd = new Random();
                for (int i = num - 1; i > 0; i--)  //在当前期之前的期数，伪造填空
                {
                    _ssc.Add(new BSSC()
                    {
                        SSC_DATE = DateTime.Now,
                        SSC_STATE = 0,
                        SSC_WRITEDT = DateTime.Now,
                        SSC_NUMBER = string.Join(",", rd.Next(10000, 99999).ToString().ToArray()),
                        SSC_NO = DateTime.Now.ToString("yyyyMMdd") + i.ToString("000")
                    });
                }
                data.SSC_DATE = DateTime.Now;
                data.SSC_WRITEDT = DateTime.Now;
                data.SSC_STATE = 0;
                data = _ssc.Add(data);
                for (int i = num + 1; i <= 120; i++)
                {
                    _ssc.Add(new BSSC()
                    {
                        SSC_DATE = DateTime.Now,
                        SSC_STATE = 0,
                        SSC_NO = DateTime.Now.ToString("yyyyMMdd") + i.ToString("000")
                    });
                }
                _ssc.Save();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ssc.SSC_NUMBER))  //如果已经有开奖数据，不覆盖
                {
                    ssc.SSC_NUMBER = data.SSC_NUMBER;
                    ssc.SSC_WRITEDT = DateTime.Now;
                    _ssc.Save();
                }
                data = ssc;
            }
            return data;
        }

        public AjaxResult<List<BSSC>> GetBSSC(BSSC bSSC)
        {
            List<BSSC> list = _ssc.Where(m => m.SSC_NUMBER != null && (m.SSC_DATE == bSSC.SSC_DATE || bSSC.SSC_DATE == null) && (m.SSC_STATE == bSSC.SSC_STATE || bSSC.SSC_STATE == null) && (m.SSC_NO == bSSC.SSC_NO || bSSC.SSC_NO == null)).ToList();
            return new AjaxResult<List<BSSC>>(list);
        }

        public AjaxResult<BSSC> GetNextSSC()
        {
            if (SSCConfigs.Config == null)
            {
                return new AjaxResult<BSSC>(false, "今日已结束");
            }
            DateTime dtYester = DateTime.Now.AddDays(-1);  //当天若已经
            BSSC ssc = _ssc.Where(m => m.SSC_NUMBER == null && m.SSC_DATE == SSCConfigs.Config.BeLoginDt).OrderBy(m => m.SSC_NO).FirstOrDefault();
            if (ssc == null || ssc.SSC_ID == 0)
            {
                return new AjaxResult<BSSC>(false, "今日已结束");
            }
            else
            {
                return new AjaxResult<BSSC>(ssc);
            }
        }


        public PageSplit<List<BSSC>> GetBSSC(int start, int limit)
        {
            var query = _ssc.GetAll().Where(m => m.SSC_NUMBER != null);
            List<BSSC> list = query.OrderByDescending(m => m.SSC_NO).Skip(start).Take(limit).ToList();
            return new PageSplit<List<BSSC>>(list, query.Count(), start, limit);
        }



        public PageSplit<List<BSSC>> GetNewSSC(int maxid)
        {
            var query = _ssc.GetAll().Where(m => m.SSC_NUMBER != null && m.SSC_ID > maxid);
            List<BSSC> list = query.OrderByDescending(m => m.SSC_NO).ToList();
            return new PageSplit<List<BSSC>>(list, query.Count(), 0, 0);
        }

        /// <summary>
        /// 填充空数据,只在首次启动程序调用
        /// </summary>
        public void CheckNullData()
        {
            BSSC Latestssc = _ssc.Where(m => m.SSC_NUMBER != null).OrderByDescending(m => m.SSC_ID).FirstOrDefault();  //最后一个填充的数据
            List<BSSC> listNull =new List<BSSC>();
            if (Latestssc != null)
            {
                listNull = _ssc.Where(m => m.SSC_NUMBER == null && m.SSC_ID < Latestssc.SSC_ID).ToList();
            }
            else
            {
                DateTime dtnow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                listNull = _ssc.Where(m => m.SSC_NUMBER == null && m.SSC_WRITEDT == null && m.SSC_DATE < dtnow).ToList();
            }
            Random rd = new Random();
            foreach (BSSC ssc in listNull)
            {
                int Numbers = rd.Next(10000, 99999);
                ssc.SSC_NUMBER = string.Join(",", Numbers.ToString().ToCharArray());
                ssc.SSC_WRITEDT = DateTime.Now;
            }
            _ssc.Save();
        }
    }
}
