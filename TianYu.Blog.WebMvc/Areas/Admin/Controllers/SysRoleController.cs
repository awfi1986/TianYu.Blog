using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Infrastructure.Enums;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysRoleController : BaseController
    {
        private ISysRoleService _sysRoleService;
        private ISysMenuService _sysMenuService;
        private ISysButtonService _sysButtonService;
        private ISysRolePowerService _sysRolePowerService;

        public SysRoleController(ISysRoleService sysRoleService, ISysMenuService sysMenuService, ISysButtonService sysButtonService, ISysRolePowerService sysRolePowerService)
        {
            this._sysRoleService = sysRoleService;
            this._sysMenuService = sysMenuService;
            this._sysButtonService = sysButtonService;
            this._sysRolePowerService = sysRolePowerService;
        }




        public IActionResult Index()
        {
            var menuList = _sysMenuService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);
            var buttonList = _sysButtonService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);
            ViewBag.ListButton = buttonList;
            return View(menuList);
        }
        public IActionResult AddEdit(int? id)
        {
            var model = new SysRole();

            if (id.HasValue)
            {
                model = _sysRoleService.FindSingle(o => o.Id == id);
            }
            return View(model);
        }



        public async Task<JsonResult> GetList([FromQuery]SysRoleListRequestModel requestModel)
        {
            var res = new AjaxResult();

            RefAsync<int> count = 0;

            var list = await _sysRoleService.FindPageListAsync(o => o.Status == StatusEnum.Effective, requestModel.Page, requestModel.Limit, count);

            res.Data = list;
            res.Curr = requestModel.Page;
            res.Count = count;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SaveSysRoleRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (requestModel.RoleName.IsNullOrWhiteSpace())
            {
                res.Message = "用户角色不能为空";
                return Json(res);
            }

            var model = requestModel.MapTo<SysRole>();


            if (requestModel.Id == 0)
            {
                model.CreateGuid = base.LoginUserGuid;
                model.CreateTime = DateTime.Now;
                model.Creator = base.LoginUserName;

                await _sysRoleService.InsertAsync(model);
            }
            else
            {
                await _sysRoleService.UpdateAsync(o => new SysRole()
                {
                    RoleName = model.RoleName,
                    Enabled = model.Enabled,
                    Modifier = LoginUserName,
                    ModifyGuid = LoginUserGuid,
                    ModifyTime = DateTime.Now
                }, x => x.Id == model.Id);
            }

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(int id)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            bool result = await _sysRoleService.UpdateAsync(o => new SysRole() { Status = Infrastructure.Enums.StatusEnum.Invalid }, o => o.Id == id);

            if (result)
            {
                res.Code = ResultCode.Succeed;
                return Json(res);
            }
            else
            {
                res.Code = ResultCode.Failure;
                res.Message = "重置失败";
                return Json(res);
            }
        }
        public async Task<JsonResult> GetRolePower(int id)
        {
            var list = await _sysRolePowerService.FindListByClauseAsync(x => x.RoleId == id);

            return Json(list);
        }
        public async Task<JsonResult> SaveRolePower(int id, string menuIds, string buttonIds)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            List<SysRolePower> list = new List<SysRolePower>();
            foreach (var item in menuIds.Split(','))
            {
                list.Add(new SysRolePower()
                {
                    RoleId = id,
                    PowerId = item.ToInt(),
                    PowerType = 1
                });
            }
            foreach (var item in buttonIds.Split(','))
            {
                list.Add(new SysRolePower()
                {
                    RoleId = id,
                    PowerId = item.ToInt(),
                    PowerType = 2
                });
            }

            _sysRolePowerService.DeleteQueue(a => a.RoleId == id);

            if (list.Count > 0)
            {
                _sysRolePowerService.InsertQueue(list);
            }
            await _sysRolePowerService.SaveQueuesAsync();

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
    }
}