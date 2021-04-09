using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Infrastructure.Enums;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysButtonController : BaseController
    {
        private ISysButtonService _sysButtonService;
        private ISysRolePowerService _sysRolePowerService;
        public SysButtonController(ISysButtonService sysButtonService, ISysRolePowerService sysRolePowerService)
        {
            _sysButtonService = sysButtonService;
            _sysRolePowerService = sysRolePowerService;
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEdit(int? id, int? mid)
        {
            var model = new SysButton()
            {
                Status = 0,
                GroupId = 1,
                IsToolbar = 1,
                BackColor = "",
                SizeStyle = "layui-btn-sm"
            };
            if (mid.HasValue)
            {
                model.MenuId = mid.Value;
            }

            if (id.HasValue)
            {
                model = _sysButtonService.FindById(id);
            }
            return View(model);
        }


        public async Task<JsonResult> Save(SysButton model)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (model.Id > 0)
            {
                model.Modifier = LoginUserName;
                model.ModifyGuid = LoginUserGuid;
                model.ModifyTime = DateTime.Now;
                await _sysButtonService.UpdateAsync(model);
            }
            else
            {
                model.CreateTime = DateTime.Now;
                model.CreateGuid = LoginUserGuid;
                model.Creator = LoginUserName;
                long result = await _sysButtonService.InsertAsync(model);

                model.Id = (int)result;
            }
            res.Data = model;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(int id)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            _sysButtonService.DeleteQueue(x => x.Id == id);
            _sysRolePowerService.DeleteQueue(o => o.PowerType == 2 && o.PowerId == id);
            await _sysRolePowerService.SaveQueuesAsync();
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> UpdateSort(int id, int sort)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            await _sysButtonService.UpdateAsync(o => new SysButton() { Sort = sort }, x => x.Id == id);

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
    }
}