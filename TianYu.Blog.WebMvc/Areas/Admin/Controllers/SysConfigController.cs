using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysConfigController : BaseController
    {
        private ISysConfigService _sysConfigService;

        public SysConfigController(ISysConfigService sysConfigService)
        {
            this._sysConfigService = sysConfigService;
        }




        public IActionResult Index()
        {
            var entity = new SysConfigRequestModel();
            var en = _sysConfigService.FindByClause(o => o.ConfigCode == "baseConfig");
            if (en != null)
            {
                if (!en.ConfigContent.IsNullOrWhiteSpace())
                {
                    entity = JsonHelper.DeserializeObject<SysConfigRequestModel>(en.ConfigContent);
                }
            }
            return View(entity);
        }

        public async Task<JsonResult> Save(SysConfigRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            string configContent = JsonHelper.SerializeObject(requestModel);

            await _sysConfigService.UpdateAsync(o => new SysConfig() { ConfigContent = configContent }, x => x.ConfigCode == "baseConfig");

            res.Code = ResultCode.Succeed;
            return Json(res);
        }

    }
}