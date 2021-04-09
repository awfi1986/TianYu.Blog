using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Domain.ViewModel.Response;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysUserController : BaseController
    {
        private ISysUserService _sysUserService;
        private ISysRoleService _sysRoleService;
        private ISysUserRoleService _sysUserRoleService;

        public SysUserController(ISysUserService sysUserService, ISysRoleService sysRoleService, ISysUserRoleService sysUserRoleService)
        {
            this._sysUserService = sysUserService;
            this._sysRoleService = sysRoleService;
            this._sysUserRoleService = sysUserRoleService;
        }



        public IActionResult Index()
        {
            ViewBag.RoleList = _sysRoleService.FindListByClause(o => o.Enabled == true && o.Status == Infrastructure.Enums.StatusEnum.Effective);
            return View();
        }
        public IActionResult AddEdit(string guid)
        {
            var model = new SysUser();
            ViewBag.RoleList = _sysRoleService.FindListByClause(o => o.Status == Infrastructure.Enums.StatusEnum.Effective);

            if (!string.IsNullOrWhiteSpace(guid))
            {
                model = _sysUserService.FindSingle(o => o.Guid == guid);
            }
            return View(model);
        }
        public IActionResult SetUserinfo()
        {
            SysUser model = _sysUserService.FindById(LoginUserGuid);

            return View(model);
        }



        public async Task<JsonResult> GetList([FromQuery]SysUserListRequestModel requestModel)
        {
            var res = new AjaxResult();

            RefAsync<int> count = 0;

            var list = await _sysUserService.FindPageListAsync(requestModel.KeyWords, requestModel.RoleId, requestModel.Page, requestModel.Limit, count);

            if (list.Any())
            {
                var newList = list.MapToList<SysUserResponseModel>();

                var userGuids = list.Select(x => x.Guid);

                var roleList = _sysUserRoleService.Find<SysRole, SysUserRole>((a, b) => new JoinQueryInfos(JoinType.Left, a.Id == b.RoleId))
                      .Where((a, b) => userGuids.Contains(b.UserGuid))
                      .Select((a, b) => new { a.RoleName, b.UserGuid }).ToList();


                if (roleList.Any())
                {
                    newList.ForEach(m =>
                    {
                        var roleName = roleList.Where(x => x.UserGuid == m.Guid).Select(x => x.RoleName).ToList();

                        if (roleName != null)
                        {
                            m.RoleName = string.Join(",", roleName);
                        }
                    });
                }

                res.Data = newList;
            }

            res.Curr = requestModel.Page;
            res.Count = count;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SaveSysUserRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (requestModel.Guid.IsNullOrWhiteSpace())
            {
                if (requestModel.UserName.IsNullOrWhiteSpace())
                {
                    res.Message = "用户名不能为空";
                    return Json(res);
                }
                if (requestModel.UserPwd.IsNullOrWhiteSpace())
                {
                    res.Message = "密码不能为空";
                    return Json(res);
                }

                if (_sysUserService.Count(o => o.UserName == requestModel.UserName && o.Status == 0) > 0)
                {
                    res.Message = $"用户名[{requestModel.UserName}]已存在，不能重复添加";
                    return Json(res);
                }
            }
            if (requestModel.UserRoleList == "[]")
            {
                res.Message = "请选择用户角色";
                return Json(res);
            }

            var model = requestModel.MapTo<SysUser>();

            var userRoleList = new List<SysUserRole>();

            if (model.Guid.IsNullOrWhiteSpace())
            {
                model.Guid = Utils.NewStrGuid();
            }

            int[] array = JsonConvert.DeserializeObject<int[]>(requestModel.UserRoleList);
            for (int i = 0; i < array.Length; i++)
            {
                userRoleList.Add(new SysUserRole()
                {
                    RoleId = array[i],
                    UserGuid = model.Guid
                });
            }

            if (requestModel.Guid.IsNullOrWhiteSpace())
            {
                string saltValue = Guid.NewGuid().ToString().Substring(0, 10);
                string userPwd = EnDecrypt.SHA1Hash(requestModel.UserPwd + saltValue);

                model.UserPwd = userPwd;
                model.SaltValue = saltValue;
                model.CreateGuid = base.LoginUserGuid;
                model.CreateTime = DateTime.Now;
                model.Creator = base.LoginUserName;

                _sysUserService.InsertQueue(model);
                _sysUserRoleService.InsertQueue(userRoleList); 
            }
            else
            {
                _sysUserService.UpdateQueue(o => new SysUser()
                {
                    TrueName = model.TrueName,
                    Enabled = model.Enabled,
                    Modifier = LoginUserName,
                    ModifyGuid = LoginUserGuid,
                    ModifyTime = DateTime.Now
                }, x => x.Guid == model.Guid);
                _sysUserRoleService.DeleteQueue(o => o.UserGuid == model.Guid);
                _sysUserRoleService.InsertQueue(userRoleList); 
            }
            await _sysUserRoleService.SaveQueuesAsync();

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(string[] guids)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            bool result = await _sysUserService.UpdateAsync(o => new SysUser()
            {
                Status = Infrastructure.Enums.StatusEnum.Invalid,
                Modifier = LoginUserName,
                ModifyGuid = LoginUserGuid,
                ModifyTime = DateTime.Now
            }, o => guids.Contains(o.Guid));

            if (result)
            {
                res.Code = ResultCode.Succeed;
                return Json(res);
            }
            else
            {
                res.Code = ResultCode.Failure;
                res.Message = "删除失败";
                return Json(res);
            }
        }
        public async Task<JsonResult> ResetPassword(string[] guids)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            string newSaltValue = Utils.NewGuid().ToString();

            string newPwd = EnDecrypt.SHA1Hash("123456" + newSaltValue);

            bool result = await _sysUserService.UpdateAsync(o => new SysUser() { UserPwd = newPwd, SaltValue = newSaltValue }, x => guids.Contains(x.Guid));

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
        public async Task<JsonResult> SaveUserInfo(SysUserSaveRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (!string.IsNullOrWhiteSpace(requestModel.OldUserPwd) && string.IsNullOrWhiteSpace(requestModel.UserPwd))
            {
                res.Message = "新密码不能为空";
                return Json(res);
            }

            var user = _sysUserService.FindById(base.LoginUserGuid);

            if (user == null)
            {
                res.Message = "当前账号不存在或已补删除";
                return Json(res);
            }
            if (!string.IsNullOrWhiteSpace(requestModel.OldUserPwd))
            {
                var oldPwd = EnDecrypt.SHA1Hash(requestModel.OldUserPwd + user.SaltValue);

                if (oldPwd != user.UserPwd)
                {
                    res.Message = "输入的旧密码错误";
                    return Json(res);
                }
                user.UserPwd = EnDecrypt.SHA1Hash(requestModel.UserPwd + user.SaltValue);
            }

            user.TrueName = requestModel.TrueName;
            var result = await _sysUserService.UpdateAsync(user);

            if (result)
            {
                res.Code = ResultCode.Succeed;
            }
            return Json(res);
        }
    }
}