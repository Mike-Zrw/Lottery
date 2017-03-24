using Lottery.Core.DataModel;
using Lottery.Core.DTO.Common;
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
        public BSSC AddFromRemote(BSSC data)
        {
            BSSC ssc = _ssc.Where(m => m.SSC_NO == data.SSC_NO).FirstOrDefault();
            if (ssc == null || ssc.SSC_ID == 0)
            {
                data.SSC_DATE = DateTime.Now;
                data.SSC_WRITEDT = DateTime.Now;
                data.SSC_STATE = 0;
                data = _ssc.Add(data);
                int num = Convert.ToInt32(data.SSC_NO.Substring(8));
                _ssc.Save();
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
                ssc.SSC_NUMBER = data.SSC_NUMBER;
                ssc.SSC_WRITEDT = DateTime.Now;
                _ssc.Save();
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
            BSSC ssc = _ssc.Where(m => m.SSC_NUMBER == null && m.SSC_DATE == DateTime.Now).OrderBy(m => m.SSC_NO).FirstOrDefault();
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
            var query = _ssc.GetAll().Where(m => m.SSC_NUMBER != null&&m.SSC_ID>maxid);
            List<BSSC> list = query.OrderByDescending(m => m.SSC_NO).ToList();
            return new PageSplit<List<BSSC>>(list, query.Count(), 0, 0);
        }
    }
}
