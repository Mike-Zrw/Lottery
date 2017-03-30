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
    public class MoneyService : IMoneyService
    {
        private ILotteryRepository _repository;
        private ICrudRepository<BUser> _use;
        private ICrudRepository<PayOnLine> _pol;
        private ICrudRepository<BUserMoney> _usm;
        private ICrudRepository<BChangeMoney> _cgm;
        private ICrudRepository<BDrawMoney> _dam;
        public MoneyService(ILotteryRepository repository)
        {
            _repository = repository;
            _use = repository.GetCrudRepository<BUser>();
            _pol = repository.GetCrudRepository<PayOnLine>();
            _usm = repository.GetCrudRepository<BUserMoney>();
            _cgm = repository.GetCrudRepository<BChangeMoney>();
            _dam = repository.GetCrudRepository<BDrawMoney>();
        }
        public AjaxResult<string> AddMoney(PayOnLine pol)
        {
            BUserMoney usm = _usm.Where(m => m.USM_USE_ID == pol.POL_USE_ID).FirstOrDefault();
            _repository.BeginTransaction();
            try
            {
                decimal oldMoney = 0;
                if (usm == null)  //支付方式为账户余额
                {
                    _usm.Add(new BUserMoney() { USM_MONEY = pol.POL_MONEY, USM_USE_ID = pol.POL_USE_ID });
                }
                else
                {
                    oldMoney = usm.USM_MONEY;
                    usm.USM_MONEY += pol.POL_MONEY;
                }
                _pol.Add(pol);
                _cgm.Add(new BChangeMoney() { CGM_MONEY = pol.POL_MONEY, CGM_BEFOREMONEY = oldMoney, CGM_AFTERMONEY = oldMoney + pol.POL_MONEY, CGM_CREATETIME = DateTime.Now, CGM_DESC = "app充值" });
                _cgm.Save();
                _repository.Commit();
                return new AjaxResult<string>((oldMoney + pol.POL_MONEY) + "");
            }
            catch (Exception)
            {
                _repository.RollBack();
                return new AjaxResult<string>(false, "操作失败");
            }
        }
    }
}
