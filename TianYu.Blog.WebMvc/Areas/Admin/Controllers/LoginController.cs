using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TianYu.Core.Common;
using TianYu.Blog.Service;
using Microsoft.AspNetCore.Http;
using TianYu.Core.Log;
using TianYu.Blog.Domain.DomainModel;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private ISysUserService _sysUserService;
        private ISysButtonService _sysButtonService;
        private ISysLoginLogService _sysLoginLogService;
        private IHttpContextAccessor _accessor;

        public LoginController(ISysUserService sysUserService, ISysButtonService sysButtonService, ISysLoginLogService sysLoginLogService, IHttpContextAccessor accessor)
        {
            this._sysUserService = sysUserService;
            this._sysButtonService = sysButtonService;
            this._sysLoginLogService = sysLoginLogService;
            this._accessor = accessor;
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            var userName = HttpContext.Session.GetString("Admin_UserName");
            var trueName = HttpContext.Session.GetString("Admin_TrueName");
            Task.Run(() =>
            {
                _sysLoginLogService.InsertAsync(new SysLoginLog()
                {
                    ActionType = 2,
                    ExecResult = "退出成功",
                    ExecTime = DateTime.Now,
                    ExecIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    ExecContent = $"退出账号：{userName}； ",
                    Operator = userName
                });
            });

            HttpContext.Session.Clear();
            return Redirect("~/admin/login/index");
        }




        public async Task<JsonResult> Login(string userName, string userPwd, string captcha)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            string code = HttpContext.Session.GetString("Captcha");
            HttpContext.Session.Remove("Captcha");

            if (userName.IsNullOrEmpty())
            {
                res.Message = "请输入用户名";
                return LoginResult(res, userName);
            }
            if (userPwd.IsNullOrEmpty())
            {
                res.Message = "请输入密码";
                return LoginResult(res, userName);
            }
            if (captcha.IsNullOrEmpty())
            {
                res.Message = "请输入验证码";
                return LoginResult(res, userName);
            }
            if (code.IsNullOrWhiteSpace())
            {
                res.Message = "验证码失效";
                return LoginResult(res, userName);
            }
            if (!captcha.ToUpper().Equals(code.ToUpper()))
            {
                res.Message = "输入的验证码错误";
                return LoginResult(res, userName);
            }

            var model = await _sysUserService.FindSingleAsync(o => o.UserName == userName);
            if (model != null || model.Status == 0)
            {
                userPwd = EnDecrypt.SHA1Hash(userPwd + model.SaltValue);
                if (userPwd != model.UserPwd)
                {
                    res.Message = "用户名或密码错误";
                    return LoginResult(res, userName);
                }

                if (model.Enabled == Infrastructure.Enums.EnabledEnum.Enable)
                {
                    HttpContext.Session.SetString("Admin_UserId", model.Guid);
                    HttpContext.Session.SetString("Admin_UserName", model.UserName);
                    HttpContext.Session.SetString("Admin_TrueName", model.TrueName);

                    var userButtonList = _sysButtonService.FindUserButton(model.Guid);
                    var userButtonListStr = string.Empty;

                    if (userButtonList != null)
                    {
                        userButtonListStr = JsonHelper.SerializeObject(userButtonList);
                    }
                    HttpContext.Session.SetString("Admin_UserButtonListStr", userButtonListStr);

                    //model.modify_time = DateTime.Now; 
                    //await _sysUserService.Update(model);
                }
                else
                {
                    res.Message = "该账号已被禁用，请联系管理员";
                    return LoginResult(res, userName);
                }
            }
            else
            {
                res.Message = "用户名或密码错误";
                return LoginResult(res, userName);
            }

            res.Code = ResultCode.Succeed;
            return LoginResult(res, userName);
        }


        private JsonResult LoginResult(AjaxResult res, string account)
        {
            string errorMessage = string.Empty;
            if (res.Code != ResultCode.Succeed)
            {
                errorMessage = $"；失败原因：{res.Message}";
            }
            _sysLoginLogService.InsertAsync(new SysLoginLog()
            {
                ActionType = 1,
                ExecResult = res.Code == ResultCode.Succeed ? "登录成功" : $"登录失败{errorMessage}",
                ExecTime = DateTime.Now,
                ExecContent = "",
                ExecIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                Operator = account
            });

            return Json(res);
        }
    }
}