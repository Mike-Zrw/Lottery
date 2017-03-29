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
    /// <summary>
    /// 购彩操作
    /// </summary>
    public class BSSC_DUE_BUYService : IBSSC_DUE_BUYService
    {
        private ILotteryRepository _repository;
        private ICrudRepository<BSSC> _ssc;
        private ICrudRepository<RSSC_TYPE> _rst;
        private ICrudRepository<BSSC_DUE_BUY> _scd;
        private ICrudRepository<BUserMoney> _usm;
        private ICrudRepository<BUser> _use;
        private ICrudRepository<BDeskUser> _due;
        public BSSC_DUE_BUYService(ILotteryRepository repository)
        {
            _repository = repository;
            _ssc = repository.GetCrudRepository<BSSC>();
            _rst = repository.GetCrudRepository<RSSC_TYPE>();
            _scd = repository.GetCrudRepository<BSSC_DUE_BUY>();
            _usm = repository.GetCrudRepository<BUserMoney>();
            _use = repository.GetCrudRepository<BUser>();
            _due = repository.GetCrudRepository<BDeskUser>();
        }
        public AjaxResult<string> Buy(List<BSSC_DUE_BUY> lists, string SSC_NO)
        {
            BSSC ssc = _ssc.Where(m => m.SSC_NO == SSC_NO).FirstOrDefault();
            if (ssc == null)
            {
                return new AjaxResult<string>(false, "您的账户余额不足");
            }
            else if (!string.IsNullOrWhiteSpace(ssc.SSC_NUMBER))
            {
                return new AjaxResult<string>(false, "当前期彩票已开奖,请购买下一期彩票");
            }
            List<BSSC_DUE_BUY> listAdd = lists.GroupBy(m => new { m.SCD_RST_ID, m.SCD_NUMBERS, m.SCD_DUE_ID, m.SCD_DAT_ID }).Select(m => new BSSC_DUE_BUY()
            {
                SCD_DAT_ID = m.Key.SCD_DAT_ID,
                SCD_NUMBERS = m.Key.SCD_NUMBERS,
                SCD_RST_ID = m.Key.SCD_RST_ID,
                SCD_DUE_ID = m.Key.SCD_DUE_ID,
                SCD_SSC_ID = ssc.SSC_ID,
                SCD_STATE = 0,
                SCD_DATE = DateTime.Now,
                SCD_TIMES = m.Sum(s => s.SCD_TIMES)
            }).ToList();
            int buyCount = listAdd.Sum(m => m.SCD_TIMES);
            BDeskUser due = _due.Where(m => m.DUE_USE_ID == listAdd[0].SCD_DUE_ID).FirstOrDefault();
            BUser use = _use.Where(m => m.USE_ID == due.DUE_USE_ID).FirstOrDefault();
            BUserMoney usm = _usm.Where(m => m.USM_USE_ID == use.USE_ID).FirstOrDefault();
            if (usm == null || usm.USM_MONEY < buyCount * 2 && listAdd[0].SCD_DAT_ID == 1)  //支付方式为账户余额
            {
                return new AjaxResult<string>(false, "您的账户余额不足");
            }
            _repository.BeginTransaction();
            try
            {
                _scd.AddRange(listAdd);
                _scd.Save();
                usm.USM_MONEY = usm.USM_MONEY - buyCount * 2;
                _usm.Save();
                _repository.Commit();
                return new AjaxResult<string>("购买成功!");
            }
            catch (Exception)
            {
                _repository.RollBack();
                return new AjaxResult<string>(false, "操作失败");
            }
        }
    }
}
