using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Linq;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Domain.ViewModel.Response;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class SysDictionaryController : BaseController
    {
        private ISysDictionaryService _sysDictionaryService;

        public SysDictionaryController(ISysDictionaryService sysDictionaryService)
        {
            this._sysDictionaryService = sysDictionaryService;
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEdit(int? id, int? parentId)
        {
            var model = new SysDictionary();

            if (id.HasValue)
            {
                model = _sysDictionaryService.FindSingle(o => o.Id == id);
            }
            else
            {
                model.ParentId = parentId.Value;
            }
            return View(model);
        }



        public async Task<JsonResult> GetList([FromQuery]QuerySysDictionaryRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            RefAsync<int> count = 0;

            var list = await _sysDictionaryService.FindPageListAsync(requestModel, count);

            res.Data = list;

            res.Curr = requestModel.Page;
            res.Count = count;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SaveSysDictionaryRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (requestModel.DictionaryName.IsNullOrWhiteSpace())
            {
                res.Message = "字典名称不能为空";
                return Json(res);
            }
            if (requestModel.DictionaryCode.IsNullOrWhiteSpace())
            {
                res.Message = "字典编码不能为空";
                return Json(res);
            }

            var count = _sysDictionaryService.Count(o => o.Id != requestModel.Id && o.ParentId == requestModel.ParentId && o.DictionaryName == requestModel.DictionaryName);
            if (count > 0)
            {
                res.Message = $"字典名称[{requestModel.DictionaryName}]已存在，不能重复添加";
                return Json(res);
            }
            count = _sysDictionaryService.Count(o => o.Id != requestModel.Id && o.ParentId == requestModel.ParentId && o.DictionaryCode == requestModel.DictionaryCode);
            if (count > 0)
            {
                res.Message = $"字典编码[{requestModel.DictionaryCode}]已存在，不能重复添加";
                return Json(res);
            }
            if (requestModel.Id <= 0)
            {
                var model = requestModel.MapTo<SysDictionary>();

                model.CreateGuid = base.LoginUserGuid;
                model.CreateTime = DateTime.Now;
                model.Creator = base.LoginUserName;

                await _sysDictionaryService.InsertAsync(model);
            }
            else
            {
                await _sysDictionaryService.UpdateAsync(o => new SysDictionary()
                {
                    DictionaryCode = requestModel.DictionaryCode,
                    DictionaryName = requestModel.DictionaryName,
                    Sort = requestModel.Sort,
                    Modifier = LoginUserName,
                    ModifyGuid = LoginUserGuid,
                    ModifyTime = DateTime.Now
                }, x => x.Id == requestModel.Id);
            }

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(int[] ids)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            bool result = await _sysDictionaryService.UpdateAsync(o => new SysDictionary()
            {
                Status = Infrastructure.Enums.StatusEnum.Invalid,
                Modifier = LoginUserName,
                ModifyGuid = LoginUserGuid,
                ModifyTime = DateTime.Now
            }, o => ids.Contains(o.Id));

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
    }
}