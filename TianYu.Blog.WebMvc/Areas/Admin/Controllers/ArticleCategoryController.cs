using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Infrastructure.Enums;
using TianYu.Blog.Service;
using TianYu.Core.Common;
using TianYu.Core.DataBase;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class ArticleCategoryController : BaseController
    {
        private IArticleCategoryService _articleCategoryService;
        public ArticleCategoryController(IArticleCategoryService articleCategoryService)
        {
            this._articleCategoryService = articleCategoryService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEdit(int? id, int? pid, int? level)
        {
            var model = new ArticleCategory()
            {
                Id = id.HasValue ? id.Value : -1,
                ParentId = pid.HasValue ? pid.Value : -1,
                Level = level.HasValue ? level.Value : 1,
                Status = 0
            };

            var list = _articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective).ToList();

            var menu = new ArticleCategory()
            {
                Id = -1,
                Name = "顶级",
                Level = 1,
                ParentId = -1,
                Sort = -1
            };
            list.Add(menu);
            var items = list.Select(t => new { id = t.Id, name = t.Name, pId = t.ParentId, open = t.ParentId == -1 });
            ViewBag.TreeJson = items.ToJsonString();

            if (id.HasValue && id > 0)
            {
                model = _articleCategoryService.FindSingle(o => o.Id == id);
            }
            ViewBag.ParentName = list.FirstOrDefault(t => t.Id == model.ParentId).Name;
            return View(model);
        }




        public async Task<JsonResult> GetList([FromQuery]ArticleCategoryListRequestModel requestModel)
        {
            var res = new AjaxResult();

            //var filter = PredicateBuilder.True<ArticleCategory>();
            //filter = filter.And(x => x.Status== StatusEnum.Effective);

            //if (!requestModel.KeyWords.IsNullOrWhiteSpace())
            //{
            //    filter = filter.And(x => x.Name.Contains(requestModel.KeyWords));
            //}

            var filter = new List<IConditionalModel>();
            filter.Add(new ConditionalModel() { FieldName = "status", ConditionalType = ConditionalType.Equal, FieldValue = ((int)StatusEnum.Effective).ToString() });

            if (!requestModel.KeyWords.IsNullOrWhiteSpace())
            {
                filter.Add(new ConditionalModel() { FieldName = "name", ConditionalType = ConditionalType.Like, FieldValue = requestModel.KeyWords });
            }

            var list = await _articleCategoryService.FindListByClauseAsync(filter, o => o.Sort, OrderByType.Asc);

            res.Data = list;

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SaveArticleCategoryRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (requestModel.Id > 0)
            {
                if (requestModel.Name.IsNullOrWhiteSpace())
                {
                    res.Message = "分类名称不能为空";
                    return Json(res);
                }

                if (_articleCategoryService.Count(o => o.Id != requestModel.Id && o.Name == requestModel.Name && o.Status == 0 && o.ParentId == requestModel.ParentId) > 0)
                {
                    res.Message = $"分类名称[{requestModel.Name}]已存在，不能重复添加";
                    return Json(res);
                }
            }

            var model = requestModel.MapTo<ArticleCategory>();

            if (requestModel.Id > 0)
            {
                await _articleCategoryService.UpdateAsync(o => new ArticleCategory()
                {
                    Name = model.Name,
                    ParentId = model.ParentId,
                    Sort = model.Sort,
                    Level = model.Level,
                    Modifier = LoginUserName,
                    ModifyGuid = LoginUserGuid,
                    ModifyTime = DateTime.Now
                }, x => x.Id == model.Id);
            }
            else
            {
                model.CreateGuid = base.LoginUserGuid;
                model.CreateTime = DateTime.Now;
                model.Creator = base.LoginUserName;

                await _articleCategoryService.InsertAsync(model);
            }

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(int[] ids)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            bool result = await _articleCategoryService.UpdateAsync(o => new ArticleCategory()
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