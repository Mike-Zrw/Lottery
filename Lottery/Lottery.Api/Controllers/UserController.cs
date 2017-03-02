using Lottery.Api.Tasks;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Lottery.Api.Controllers
{
    public class UserController : ApiController
    {
        private IBUserService _user;
        private IBDeskUserService _duser;
        public UserController(IBUserService user, IBDeskUserService duser)
        {
            _user = user;
            _duser = duser;
        }
        /// <summary>
        /// 注册获取验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public AjaxResult<string> Register_GetYzm(string mobile)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum > 0)
            {
                return new AjaxResult<string>(false, "此手机号已经注册过");
            }
            AjaxResult<int> sendResult = new NetEaseSMS().SendMsg(mobile);
            if (sendResult.Success)
            {
                DataCache.SetCache(mobile + "zcyzm", sendResult.Data, DateTime.UtcNow.AddSeconds(60), new TimeSpan(10));
                return new AjaxResult<string>(true, "短信已经发至您的手机上"); ;
            }
            else
            {
                return new AjaxResult<string>(false, "短信发送失败：" + sendResult.Error);
            }
        }
        /// <summary>
        /// 注册验证码核对
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="yzm">验证码</param>
        /// <returns></returns>
        public AjaxResult<BDeskUserDto> Register_Reg(string mobile, string yzm)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum > 0)
                return new AjaxResult<BDeskUserDto>(false, "此手机号已经注册过");

            object cacheyzm = DataCache.GetCache(mobile + "zcyzm");
            if (cacheyzm == null || cacheyzm.ToString() != yzm)
                return new AjaxResult<BDeskUserDto>(false, "验证码输入错误");
            return _duser.Register(new BDeskUserDto() { USE_NAME = mobile, DUE_PHONE = mobile, USE_PASSWORD = "", USE_UGP_ID = 1, USE_ACTIVITY = true });
        }
        /// <summary>
        /// 登陆获取验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        [HttpGet]
        public AjaxResult<string> Login_GetYzm(string mobile)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum == 0)
            {
                return new AjaxResult<string>(false, "此手机号没有注册过");
            }
            AjaxResult<int> sendResult = new NetEaseSMS().SendMsg(mobile);
            if (sendResult.Success)
            {
                DataCache.SetCache(mobile + "dlyzm", sendResult.Data, DateTime.UtcNow.AddSeconds(60), new TimeSpan(10));
                return new AjaxResult<string>(true, "短信已经发至您的手机上");
            }
            else
            {
                return new AjaxResult<string>(false, "短信发送失败：" + sendResult.Error);
            }
        }
        /// <summary>
        /// 登陆验证码核对
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="yzm">验证码</param>
        /// <returns></returns>
        public AjaxResult<BDeskUserDto> Login_ByPhone(string mobile, string yzm)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum == 0)
                return new AjaxResult<BDeskUserDto>(false, "此手机号没有注册过");
            object cacheyzm = DataCache.GetCache(mobile + "dlyzm");
            if (cacheyzm == null || cacheyzm != yzm)
                return new AjaxResult<BDeskUserDto>(false, "验证码输入错误");
            return _duser.Register(new BDeskUserDto() { USE_NAME = mobile, DUE_PHONE = mobile, USE_PASSWORD = "", USE_UGP_ID = 1, USE_ACTIVITY = true });
        }
    }
}
