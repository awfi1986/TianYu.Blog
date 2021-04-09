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

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        private IArticleService _articleService;
        private IArticleCategoryService _articleCategoryService;
        public ArticleController(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            this._articleService = articleService;
            this._articleCategoryService = articleCategoryService;
        }



        public IActionResult Index()
        {
            var categoryList = (List<ArticleCategory>)_articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);

            ViewBag.CategoryList = GetSelectHtml(categoryList, -1, 0);

            return View();
        }
        public IActionResult AddEdit(string guid)
        {
            var model = new Article();
            if (!string.IsNullOrWhiteSpace(guid))
            {
                model = _articleService.FindById(guid);
            }
            var categoryList = (List<ArticleCategory>)_articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);

            ViewBag.CategoryList = GetSelectHtml(categoryList, -1, model.CategoryId);

            return View(model);
        }




        public async Task<JsonResult> GetList([FromQuery]ArticleListRequestModel requestModel)
        {
            var res = new AjaxResult();

            var list = await _articleService.FindPageListAsync(requestModel);

            res.Data = list;
            res.Count = requestModel.Total;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Save(SaveArticleRequestModel requestModel)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            if (requestModel.ArticleTitle.IsNullOrWhiteSpace())
            {
                res.Message = "文章标题不能为空";
                return Json(res);
            }
            if (requestModel.Content.IsNullOrWhiteSpace())
            {
                res.Message = "文章内容不能为空";
                return Json(res);
            }
            if (_articleService.Count(o => o.Guid != requestModel.Guid && o.ArticleTitle == requestModel.ArticleTitle && o.Status == 0) > 0)
            {
                res.Message = $"文章标题[{requestModel.ArticleTitle}]已存在，不能重复添加";
                return Json(res);
            }


            var model = requestModel.MapTo<Article>();

            if (!requestModel.Guid.IsNullOrWhiteSpace())
            {
                await _articleService.UpdateAsync(o => new Article()
                {
                    ArticleTitle = model.ArticleTitle,
                    IsRecommend = model.IsRecommend,
                    IsTop = model.IsTop,
                    IsOriginal = model.IsOriginal,
                    Keywords = model.Keywords,
                    Description = model.Description,
                    PulishStatus = model.PulishStatus,
                    SubTitle = model.SubTitle,
                    TitleImg = model.TitleImg,
                    CategoryId = model.CategoryId,
                    Content = model.Content,
                    Modifier = LoginUserName,
                    ModifyGuid = LoginUserGuid,
                    ModifyTime = DateTime.Now
                }, x => x.Guid == model.Guid);
            }
            else
            {
                model.Guid = Utils.NewStrGuid();
                model.CreateGuid = base.LoginUserGuid;
                model.CreateTime = DateTime.Now;
                model.Creator = base.LoginUserName;

                await _articleService.InsertAsync(model);
            }

            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        public async Task<JsonResult> Remove(string[] guids)
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            bool result = await _articleService.UpdateAsync(o => new Article()
            {
                Status = StatusEnum.Invalid,
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


        private string GetSelectHtml(List<ArticleCategory> model, int parentId, int selecedId)
        {
            var list = model.Where(x => x.ParentId == parentId);
            string result = string.Empty;
            if (list.Any())
            {
                foreach (var m in list)
                {
                    if (model.Where(x => x.ParentId == m.Id).Count() > 0)
                    {
                        result += $"<option value=\"{m.Id}\" disabled>{GetEmptyHtml(m.Level)}{m.Name}</option>";
                    }
                    else
                    {
                        result += $"<option value=\"{m.Id}\"{((m.Id == selecedId) ? "selected" : "")}>{GetEmptyHtml(m.Level)}{m.Name}</option>";
                    }

                    result += GetSelectHtml(model, m.Id, selecedId);
                }
            }

            return result;
        }

        private string GetEmptyHtml(int level)
        {
            string html = string.Empty;

            for (int i = 1; i < level; i++)
            {
                html += "&#160;&#160;&#160;&#160;";
            }

            return html;
        }
    }
}