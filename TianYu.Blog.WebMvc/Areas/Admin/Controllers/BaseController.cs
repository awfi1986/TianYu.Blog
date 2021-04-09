using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        public string LoginUserGuid { get { return HttpContext.Session.GetString("Admin_UserId"); } }
        public string LoginUserName { get { return HttpContext.Session.GetString("Admin_UserName"); } }
           
        /// <summary>
        /// 判断是否登录并获取菜单权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int menuId = Convert.ToInt32(filterContext.HttpContext.Request.Query["mid"]);
            int parentId = Convert.ToInt32(filterContext.HttpContext.Request.Query["pid"]);
             
            if (string.IsNullOrWhiteSpace(LoginUserGuid))
            { 
                //通过HTTP请求头来判断是否为Ajax请求，Ajax请求的request headers里都会有一个key为x-requested-with，值“XMLHttpRequest”
                var requestData = filterContext.HttpContext.Request.Headers.ContainsKey("x-requested-with");
                bool IsAjax = false;
                if (requestData)
                {
                    IsAjax = filterContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? true : false;
                }
                if (IsAjax)//不是异步请求则跳转页面，异步请求则返回json
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = "{\"code\":4001,\"message\":\"登录超时，请重新登录\"}",
                        StatusCode = StatusCodes.Status200OK,
                        ContentType = "application/json;charset=utf-8"
                    };
                }
                else
                {
                    filterContext.Result = Redirect("/Admin/Login/Index");
                }
            }
            else
            {
                ViewBag.Mid = menuId;
                ViewBag.Pid = parentId;
                GetUserButton(menuId);
            }
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            if (string.IsNullOrWhiteSpace(LoginUserGuid))
            {
                //return Json("");
            }
            return base.OnActionExecutionAsync(filterContext, next);
        }

        #region 获取用户按钮
        /// <summary>
        /// 获取用户按钮
        /// </summary>
        /// <param name="mid">菜单id</param>
        private void GetUserButton(int mid)
        {
            if (mid == 0) return;

            List<SysButton> list = null;// _ysButtonService.FindUserButton(LoginUserGuid, mid);
            var buttonList = HttpContext.Session.GetString("Admin_UserButtonListStr");
            if (!string.IsNullOrEmpty(buttonList))
            {
                list = JsonHelper.DeserializeObject<List<SysButton>>(buttonList);

                list = list.Where(x => x.MenuId == mid).ToList();
            }
            StringBuilder builder = new StringBuilder();
            foreach (var item in list.Where(t => t.GroupId == 1))
            {
                if (item.IsToolbar == 1)
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle} {item.BackColor}\" lay-event=\"{item.JsEvent}\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
                else
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle } {item.BackColor}\" onclick=\"{item.JsEvent}()\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
            }
            ViewBag.ButtonHtml = builder.ToString();
            builder = new StringBuilder();
            foreach (var item in list.Where(t => t.GroupId == 2))
            {
                if (item.IsToolbar == 1)
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle} {item.BackColor}\" lay-event=\"{item.JsEvent}\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
                else
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle } {item.BackColor}\" onclick=\"{item.JsEvent}()\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
            }
            ViewBag.ButtonHtml2 = builder.ToString();
            builder = new StringBuilder();
            foreach (var item in list.Where(t => t.GroupId == 3))
            {
                if (item.IsToolbar == 1)
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle} {item.BackColor}\" lay-event=\"{item.JsEvent}\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
                else
                {
                    builder.Append($"<button type=\"button\" class=\"layui-btn {item.SizeStyle } {item.BackColor}\" onclick=\"{item.JsEvent}()\"><i class=\"layui-icon {item.Icon}\"></i>{item.ButtonName}</button>");
                }
            }
            ViewBag.ButtonHtml3 = builder.ToString();
        }
        #endregion       
    }
}
