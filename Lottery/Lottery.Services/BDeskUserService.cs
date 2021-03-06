﻿using log4net;
using Lottery.Core.DataModel;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public class BDeskUserService : IBDeskUserService
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ILotteryRepository repository;
        private ICrudRepository<BUser> userRpt;
        private ICrudRepository<BDeskUser> deskUserRpt;
        private ICrudRepository<BUserMoney> _usm;

        public BDeskUserService(ILotteryRepository repository)
        {
            this.repository = repository;
            userRpt = repository.GetCrudRepository<BUser>();
            deskUserRpt = repository.GetCrudRepository<BDeskUser>();
            _usm = repository.GetCrudRepository<BUserMoney>();
        }
        public AjaxResult<BDeskUserDto> Register(BDeskUserDto user)
        {
            if (userRpt.Where(m => m.USE_NAME.ToLower().Trim().Equals(user.USE_NAME.ToLower().Trim())).Count() > 0)
                return new AjaxResult<BDeskUserDto>(false, "用户名已存在");
            else if (!string.IsNullOrWhiteSpace(user.DUE_PHONE) && deskUserRpt.Where(m => m.DUE_PHONE == user.DUE_PHONE).Count() > 0)
                return new AjaxResult<BDeskUserDto>(false, "此手机号已经注册过");
            else if (!string.IsNullOrWhiteSpace(user.DUE_WX_TOKEN) && deskUserRpt.Where(m => m.DUE_WX_TOKEN == user.DUE_WX_TOKEN).Count() > 0)
                return new AjaxResult<BDeskUserDto>(false, "此微信号已经注册过");
            else if (!string.IsNullOrWhiteSpace(user.DUE_QQ_TOKEN) && deskUserRpt.Where(m => m.DUE_QQ_TOKEN == user.DUE_QQ_TOKEN).Count() > 0)
                return new AjaxResult<BDeskUserDto>(false, "此QQ号已经注册过");
            else if (!string.IsNullOrWhiteSpace(user.DUE_WB_TOKEN) && deskUserRpt.Where(m => m.DUE_WB_TOKEN == user.DUE_WB_TOKEN).Count() > 0)
                return new AjaxResult<BDeskUserDto>(false, "此微博号已经注册过");
            try
            {
                repository.BeginTransaction();
                BUser buser = userRpt.Add(new BUser()
                {
                    USE_NAME = user.USE_NAME,
                    USE_ACTIVITY = user.USE_ACTIVITY,
                    USE_PASSWORD = Md5.MD5(user.USE_PASSWORD),
                    USE_UGP_ID = user.USE_UGP_ID
                });
                repository.Save();

                BDeskUser bdescuser = deskUserRpt.Add(new BDeskUser()
                {
                    DUE_EMAIL = user.DUE_EMAIL,
                    DUE_PHONE = user.DUE_PHONE,
                    DUE_REALNAME = user.DUE_REALNAME,
                    DUE_REGISTTIME = DateTime.Now,
                    DUE_SEX = user.DUE_SEX,
                    DUE_SUT_ID = user.DUE_SUT_ID,
                    DUE_USE_ID = buser.USE_ID,
                    DUE_USERDSPNAME = user.DUE_USERDSPNAME,
                    DUE_QQ_TOKEN = user.DUE_QQ_TOKEN,
                    DUE_WX_TOKEN = user.DUE_WX_TOKEN,
                    DUE_WB_TOKEN = user.DUE_WB_TOKEN
                });

                repository.Save();
                repository.Commit();
                return new AjaxResult<BDeskUserDto>(DeskUserToDto(buser, bdescuser));
            }
            catch (Exception ex)
            {
                repository.RollBack();
                _log.Error(ex);
                return new AjaxResult<BDeskUserDto>(false, "执行数据插入出错");
            }
        }

        private BDeskUserDto DeskUserToDto(BUser buser, BDeskUser bdescuser)
        {
            BUserMoney usm = _usm.Where(m => m.USM_USE_ID == buser.USE_ID).FirstOrDefault();
            return new BDeskUserDto()
            {
                DUE_EMAIL = bdescuser.DUE_EMAIL,
                DUE_PHONE = bdescuser.DUE_PHONE,
                DUE_REALNAME = bdescuser.DUE_REALNAME,
                DUE_REGISTTIME = bdescuser.DUE_REGISTTIME,
                DUE_SEX = bdescuser.DUE_SEX,
                DUE_SUT_ID = bdescuser.DUE_SUT_ID,
                DUE_USE_ID = buser.USE_ID,
                DUE_USERDSPNAME = bdescuser.DUE_USERDSPNAME,
                DUE_ID = bdescuser.DUE_ID,
                USE_ACTIVITY = buser.USE_ACTIVITY,
                USE_ID = buser.USE_ID,
                USE_NAME = buser.USE_NAME,
                USE_PASSWORD = buser.USE_PASSWORD,
                USE_UGP_ID = buser.USE_UGP_ID,
                DUE_QQ_TOKEN = bdescuser.DUE_QQ_TOKEN,
                DUE_WB_TOKEN = bdescuser.DUE_WB_TOKEN,
                DUE_WX_TOKEN = bdescuser.DUE_WX_TOKEN,
                USM_MONEY = usm == null ? 0 : usm.USM_MONEY
            };
        }

        public IQueryable<BDeskUserDto> FindBDeskUser(BDeskUserDto user)
        {
            var query = from du in deskUserRpt.Where(m => (m.DUE_PHONE == user.DUE_PHONE || user.DUE_PHONE == null)
                                                    && (user.DUE_QQ_TOKEN == null || user.DUE_QQ_TOKEN == m.DUE_QQ_TOKEN)
                                                    && (user.DUE_WX_TOKEN == null || user.DUE_WX_TOKEN == m.DUE_WX_TOKEN)
                                                    && (user.DUE_WB_TOKEN == null || user.DUE_WB_TOKEN == m.DUE_WB_TOKEN))
                        join u in userRpt.Where(m => (m.USE_NAME == user.USE_NAME || user.USE_NAME == null)
                                                 && (m.USE_PASSWORD == user.USE_PASSWORD || user.USE_PASSWORD == null))
                        on du.DUE_USE_ID equals u.USE_ID
                        join usm in _usm.GetAll() on u.USE_ID equals usm.USM_USE_ID into usmit
                        from usmr in usmit.DefaultIfEmpty()
                        select new BDeskUserDto
                        {
                            DUE_EMAIL = du.DUE_EMAIL,
                            USE_NAME = u.USE_NAME,
                            DUE_PHONE = du.DUE_PHONE,
                            USE_UGP_ID = u.USE_UGP_ID,
                            USE_PASSWORD = u.USE_PASSWORD,
                            USE_ID = u.USE_ID,
                            USE_ACTIVITY = u.USE_ACTIVITY,
                            DUE_ID = du.DUE_ID,
                            DUE_USERDSPNAME = du.DUE_USERDSPNAME,
                            DUE_SUT_ID = du.DUE_SUT_ID,
                            DUE_SEX = du.DUE_SEX,
                            DUE_REALNAME = du.DUE_REALNAME,
                            DUE_REGISTTIME = du.DUE_REGISTTIME,
                            DUE_USE_ID = du.DUE_USE_ID,
                            DUE_QQ_TOKEN = du.DUE_QQ_TOKEN,
                            DUE_WX_TOKEN = du.DUE_WX_TOKEN,
                            DUE_WB_TOKEN = du.DUE_WB_TOKEN,
                            USM_MONEY = usmr == null ? 0 : usmr.USM_MONEY
                        };
            return query;
        }

        public AjaxResult<BDeskUserDto> Login(string name, string pass)
        {
            BUser user = userRpt.Where(m => (m.USE_NAME == name) && (m.USE_PASSWORD == Md5.MD5(pass))).SingleOrDefault();
            if (user == null)
                return new AjaxResult<BDeskUserDto>(false, "用户名或密码错误");
            if (user.USE_ACTIVITY == false)
                return new AjaxResult<BDeskUserDto>(false, "账号已被禁用");
            BDeskUser deskuser = deskUserRpt.Where(m => m.DUE_USE_ID == user.USE_ID).SingleOrDefault();
            return new AjaxResult<BDeskUserDto>(DeskUserToDto(user, deskuser));
        }
    }
}
