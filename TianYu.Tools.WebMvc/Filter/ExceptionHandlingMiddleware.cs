using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianYu.Core.Log;

namespace TianYu.Blog.WebMvc.Filter
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                await HandleExceptionAsync(context, ex.ToString());
            }
        }
        private Task HandleExceptionAsync(HttpContext context, string msg)
        {
            LogHelper.LogError("HandleExceptionAsync", msg); 
            return context.Response.WriteAsync("ERROR");
        }
    }
}
