﻿using Lottery.Api.Tasks;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
using Lottery.Core.IServices;
using Lottery.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Lottery.Api.Controllers
{
    /// <summary>
    /// 用户操作
    /// </summary>
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
        [HttpPost]
        public AjaxResult<string> Register_GetYzm([FromBody]string mobile)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum > 0)
            {
                return new AjaxResult<string>(false, "此手机号已经注册过");
            }
            //AjaxResult<int> sendResult = new NetEaseSMS().SendMsg(mobile, 3049151);
            //if (sendResult.Success)
            //{
            //    DataCache.SetCache(mobile + "zcyzm", sendResult.Data, DateTime.UtcNow.AddSeconds(60), TimeSpan.Zero);
            //    return new AjaxResult<string>(true, "短信已经发至您的手机上"); ;
            //}
            //else
            //{
            //    return new AjaxResult<string>(false, "短信发送失败：" + sendResult.Error);
            //}
            DataCache.SetCache(mobile + "zcyzm", 1234, DateTime.UtcNow.AddSeconds(60), TimeSpan.Zero);
            return new AjaxResult<string>(true, "短信已经发至您的手机上"); ;
        }
        /// <summary>
        /// 注册验证码核对
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="yzm">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<BDeskUserDto> Register_Reg([FromBody]string Params)
        {
            dynamic ParamObj = JsonConvert.DeserializeObject(Params);
            string mobile = ParamObj.mobile;
            string yzm = ParamObj.yzm;
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
        [HttpPost]
        public AjaxResult<string> Login_GetYzm([FromBody]string mobile)
        {
            int sum = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).Count();
            if (sum == 0)
            {
                return new AjaxResult<string>(false, "此手机号没有注册过");
            }
            //AjaxResult<int> sendResult = new NetEaseSMS().SendMsg(mobile, 3064048);
            //if (sendResult.Success)
            //{
            //    DataCache.SetCache(mobile + "dlyzm", sendResult.Data, DateTime.UtcNow.AddSeconds(60), TimeSpan.Zero);
            //    return new AjaxResult<string>(true, "短信已经发至您的手机上");
            //}
            //else
            //{
            //    return new AjaxResult<string>(false, "短信发送失败：" + sendResult.Error);
            //}
            DataCache.SetCache(mobile + "dlyzm", 4321, DateTime.UtcNow.AddSeconds(60), TimeSpan.Zero);
            return new AjaxResult<string>(true, "短信已经发至您的手机上"); ;
        }
        /// <summary>
        /// 登陆验证码核对
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="yzm">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<BDeskUserDto> Login_ByPhone([FromBody]string Params)
        {
            dynamic ParamObj = JsonConvert.DeserializeObject(Params);
            string mobile = ParamObj.mobile;
            string yzm = ParamObj.yzm;
            BDeskUserDto user = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_PHONE = mobile }).FirstOrDefault();
            if (user == null)
                return new AjaxResult<BDeskUserDto>(false, "此手机号没有注册过");
            object cacheyzm = DataCache.GetCache(mobile + "dlyzm");
            if (cacheyzm == null || cacheyzm.ToString() != yzm)
                return new AjaxResult<BDeskUserDto>(false, "验证码输入错误");
            return new AjaxResult<BDeskUserDto>(user);
        }
        /// <summary>
        /// qq登陆
        /// </summary>
        /// <param name="qqtoken"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<BDeskUserDto> Login_ByQQ([FromBody]string qqtoken)
        {
            if (string.IsNullOrWhiteSpace(qqtoken))
                return null;
            BDeskUserDto user = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_QQ_TOKEN = qqtoken }).FirstOrDefault();
            if (user != null)
                return new AjaxResult<BDeskUserDto>(user);
            return _duser.Register(new BDeskUserDto() { USE_NAME = "qq" + qqtoken, DUE_QQ_TOKEN = qqtoken, USE_PASSWORD = "", USE_UGP_ID = 1, USE_ACTIVITY = true });
        }
        /// <summary>
        /// wx登陆
        /// </summary>
        /// <param name="wxtoken"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<BDeskUserDto> Login_ByWX([FromBody]string wxtoken)
        {
            if (string.IsNullOrWhiteSpace(wxtoken))
                return null;
            BDeskUserDto user = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_WX_TOKEN = wxtoken }).FirstOrDefault();
            if (user != null)
                return new AjaxResult<BDeskUserDto>(user);
            return _duser.Register(new BDeskUserDto() { USE_NAME = "wx" + wxtoken, DUE_WX_TOKEN = wxtoken, USE_PASSWORD = "", USE_UGP_ID = 1, USE_ACTIVITY = true });
        }
        /// <summary>
        /// wb登陆
        /// </summary>
        /// <param name="wbtoken"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<BDeskUserDto> Login_ByWB([FromBody]string wbtoken)
        {
            if (string.IsNullOrWhiteSpace(wbtoken))
                return null;
            BDeskUserDto user = _duser.FindBDeskUser(new Core.DTO.BDeskUserDto() { DUE_WB_TOKEN = wbtoken }).FirstOrDefault();
            if (user != null)
                return new AjaxResult<BDeskUserDto>(user);
            return _duser.Register(new BDeskUserDto() { USE_NAME = "wb" + wbtoken, DUE_WB_TOKEN = wbtoken, USE_PASSWORD = "", USE_UGP_ID = 1, USE_ACTIVITY = true });
        }
        /// <summary>
        /// 重新获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public AjaxResult<BDeskUserDto> GetUserById(int userid)
        {
            BDeskUserDto user = _duser.FindBDeskUser(new BDeskUserDto() { }).FirstOrDefault();
            if (user != null)
                return new AjaxResult<BDeskUserDto>(user);
            return new AjaxResult<BDeskUserDto>(false, "", null);
        }
    }
}
