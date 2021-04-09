using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters; 
using System.Threading.Tasks; 
using TianYu.Core.Log;

namespace TianYu.Blog.WebMvc.Filter
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.ExceptionHandled = true;

                //记录异常日志
                string controllerName = (string)context.RouteData.Values["controller"];
                string actionName = (string)context.RouteData.Values["action"];
                string msgTemplate = "在执行 controller[{0}] 的 action[{1}] 时产生异常";

                LogHelper.LogError(string.Format(msgTemplate, controllerName, actionName), context.Exception.Message);

                //通过HTTP请求头来判断是否为Ajax请求，Ajax请求的request headers里都会有一个key为x-requested-with，值“XMLHttpRequest”
                var requestData = context.HttpContext.Request.Headers.ContainsKey("x-requested-with");
                bool IsAjax = false;
                if (requestData)
                {
                    IsAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? true : false;
                }
                if (IsAjax)//不是异步请求则跳转页面，异步请求则返回json
                { 
                    context.Result = new ContentResult
                    {
                        Content = "{\"code\":500,\"message\":\"系统异常\"}",
                        StatusCode = StatusCodes.Status200OK,
                        ContentType = "application/json;charset=utf-8" 
                    };
                }
                else
                {
                    context.Result = new RedirectResult("/Error/Index");
                } 
            }
            return Task.CompletedTask;
        }
    }
}
