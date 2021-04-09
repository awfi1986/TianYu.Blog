using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Infrastructure.Enums;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysMenuController : BaseController
    {
        private ISysMenuService _sysMenuService;
        private ISysButtonService _sysButtonService;
        private ISysRolePowerService _sysRolePowerService;

        public SysMenuController(ISysMenuService sysMenuService, ISysButtonService sysButtonService, ISysRolePowerService sysRolePowerService)
        {
            this._sysMenuService = sysMenuService;
            this._sysButtonService = sysButtonService;
            this._sysRolePowerService = sysRolePowerService;
        }



        public IActionResult Index()
        {
            ViewBag.ListButton = _sysButtonService.FindListByClause(o => o.Status == StatusEnum.Effective).OrderBy(x => x.Sort).ToList();
            var list = _sysMenuService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);
            return View(list);
        }
        public IActionResult AddEdit(int? id, int? pid)
        {
            var model = new SysMenu()
            {
                Id = id.HasValue ? id.Value : 0,
                ParentId = pid.HasValue ? pid.Value : -1,
                Status = 0
            };

            List<SysMenu> list = _sysMenuService.FindListByClause(t => t.Id != model.Id && t.Status == StatusEnum.Effective).ToList();
            var menu = new SysMenu()
            {
                Id = -1,
                MenuName = "顶级",
                ParentId = -1,
                Sort = -1
            };
            list.Add(menu);
            var items = list.Select(t => new { id = t.Id, name = t.MenuName, pId = t.ParentId, open = t.ParentId == -1 });
            ViewBag.TreeJson = items.ToJsonString();
            if (id.HasValue && id > 0)
            {
                model = _sysMenuService.FindById(id);
            }
            ViewBag.MenuName = list.FirstOrDefault(t => t.Id == model.ParentId).MenuName;

            return View(model);
        }
        public IActionResult SelectIcon()
        {
            return View();
        }



        public async Task<JsonResult> GetList(string keyWords)
        {
            var res = new AjaxResult();

            var list = await _sysMenuService.FindListByClauseAsync(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);

            res.Data = list;

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SysMenu model)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (_sysMenuService.Count(o => o.Id != model.Id && o.MenuName == model.MenuName) > 0)
            { 
                res.Message = $"菜单名称[{model.MenuName}]已存在，不能重复添加";
                return Json(res);
            }

            if (model.Id > 0)
            {
                var entity = _sysMenuService.FindByClause(x => x.Id == model.Id && x.Status == 0);

                if (entity == null)
                {
                    res.Message = "菜单不存在或已被删除";
                    return Json(res);
                }
                entity.MenuName = model.MenuName;
                entity.MenuIcon = model.MenuIcon;
                entity.MenuUrl = model.MenuUrl;
                entity.ParentId = model.ParentId;
                entity.ParentCode = model.ParentCode;
                entity.Sort = model.Sort;
                entity.Modifier = LoginUserName;
                entity.ModifyGuid = LoginUserGuid;
                entity.ModifyTime = DateTime.Now;

                await _sysMenuService.UpdateAsync(entity);
            }
            else
            {
                model.Enabled = true;
                model.CreateTime = DateTime.Now;
                model.CreateGuid = LoginUserGuid;
                model.Creator = LoginUserName;
                await _sysMenuService.InsertAsync(model);
            }
            res.Data = model;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(int id)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            _sysMenuService.UpdateQueue(o => new SysMenu()
            {
                Status = StatusEnum.Invalid,
                Modifier = LoginUserName,
                ModifyGuid = LoginUserGuid,
                ModifyTime = DateTime.Now
            }, x => x.Id == id);
            _sysRolePowerService.DeleteQueue(o => o.PowerType == 1 && o.PowerId == id);
            await _sysRolePowerService.SaveQueuesAsync();

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> UpdateSort(int id, int sort)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            await _sysMenuService.UpdateAsync(o => new SysMenu() { Sort = sort }, x => x.Id == id);

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
    }
}