
//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2021-01-18 14:51:29 
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//-------------------------------------------------------------------- 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TianYu.Blog.Service;
using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysLoginLogController : BaseController
    {
        private ISysLoginLogService _sysLoginLogService;
        public SysLoginLogController(ISysLoginLogService sysLoginLogService)
        {
            this._sysLoginLogService = sysLoginLogService;
        }



        public IActionResult Index()
        {
            return View();
        }



        public async Task<JsonResult> GetList([FromQuery]BaseRequest requestModel)
        {
            var res = new AjaxResult();

            if (!requestModel.KeyWords.IsNullOrEmpty())
                requestModel.KeyWords = requestModel.KeyWords.Trim();

            var list = await _sysLoginLogService.FindPageListAsync(requestModel.KeyWords, requestModel.Page, requestModel.Limit, requestModel.Total);

            res.Data = list;
            res.Count = requestModel.Total;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }

    }
}

