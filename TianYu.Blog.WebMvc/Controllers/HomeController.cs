using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Infrastructure.Enums;
using TianYu.Blog.Service;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Controllers
{
    public class HomeController : BaseController
    {
        private IArticleService _articleService;
        private IArticleCategoryService _articleCategoryService;
        private ISysConfigService _sysConfigService;
        public HomeController(IArticleService articleService, IArticleCategoryService articleCategoryService, ISysConfigService sysConfigService)
        {
            this._articleService = articleService;
            this._articleCategoryService = articleCategoryService;
            this._sysConfigService = sysConfigService;
        }



        public IActionResult Index(int categoryId, string articleTitle, int pageIndex = 1)
        {
            int total = 0;

            var list = _articleService.FindPageList(articleTitle, categoryId, pageIndex, 20, ref total);

            ViewBag.Paging = GeneratePages(pageIndex, 20, "/Home/Index?pageIndex=", total);
            ViewBag.CategoryId = categoryId;

            ViewBag.RecommendList = _articleService.FindListByClause(o => o.IsRecommend == true && o.Status == StatusEnum.Effective && o.PulishStatus == 1, o => o.CreateTime, OrderByType.Desc);
            ViewBag.HotList = _articleService.FindHotList();
            ViewBag.MenuList = _articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);

            var en = _sysConfigService.FindByClause(o => o.ConfigCode == "baseConfig");
            if (en != null)
            {
                if (!en.ConfigContent.IsNullOrWhiteSpace())
                {
                    var entity = JsonHelper.DeserializeObject<SysConfigRequestModel>(en.ConfigContent);

                    ViewBag.Title = entity.SysName;
                    ViewBag.Keywords = entity.Keywords;
                    ViewBag.Description = entity.Description;
                }
            }

            return View(list);
        }
        public IActionResult Detail(string guid)
        {
            var entity = _articleService.FindSingle(o => o.Guid == guid && o.PulishStatus == 1 && o.Status == StatusEnum.Effective);

            if (entity != null)
            {
                ViewBag.CategoryId = entity.CategoryId;

                //浏览量
                _articleService.UpdateAsync(o => new Article() { PVCount = entity.PVCount + 1 }, x => x.Guid == guid);
                //上一篇、下一篇
                var upNextList = _articleService.FindUpNext(guid);
                if (upNextList.Any(x => x.Tag == 1))
                {
                    var en = upNextList.Where(x => x.Tag == 1).FirstOrDefault();
                    ViewBag.UpGuid = en.Guid;
                    ViewBag.UpTitle = en.ArticleTitle;
                }
                if (upNextList.Any(x => x.Tag == 2))
                {
                    var en = upNextList.Where(x => x.Tag == 2).FirstOrDefault();
                    ViewBag.NextGuid = en.Guid;
                    ViewBag.NextTitle = en.ArticleTitle;
                }
            }

            if (entity == null || entity.Keywords.IsNullOrWhiteSpace() || entity.Description.IsNullOrWhiteSpace())
            {
                var configEn = _sysConfigService.FindByClause(o => o.ConfigCode == "baseConfig");
                if (configEn != null)
                {
                    if (!configEn.ConfigContent.IsNullOrWhiteSpace())
                    {
                        var seoEn = JsonHelper.DeserializeObject<SysConfigRequestModel>(configEn.ConfigContent);

                        entity.Keywords = seoEn.Keywords;
                        entity.Description = seoEn.Description;
                    }
                }
            }

            ViewBag.Title = entity.ArticleTitle;
            ViewBag.Keywords = entity.Keywords;
            ViewBag.Description = entity.Description;

            ViewBag.RecommendList = _articleService.FindListByClause(o => o.IsRecommend == true && o.Status == StatusEnum.Effective && o.PulishStatus == 1, o => o.CreateTime, OrderByType.Desc);
            ViewBag.HotList = _articleService.FindHotList();
            ViewBag.MenuList = _articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);


            return View(entity);
        }
        public IActionResult Preview(string guid)
        {
            var entity = _articleService.FindSingle(o => o.Guid == guid);

            if (entity != null)
            {
                ViewBag.CategoryId = entity.CategoryId;

                //上一篇、下一篇
                var upNextList = _articleService.FindUpNext(guid);
                if (upNextList.Any(x => x.Tag == 1))
                {
                    var en = upNextList.Where(x => x.Tag == 1).FirstOrDefault();
                    ViewBag.UpGuid = en.Guid;
                    ViewBag.UpTitle = en.ArticleTitle;
                }
                if (upNextList.Any(x => x.Tag == 2))
                {
                    var en = upNextList.Where(x => x.Tag == 2).FirstOrDefault();
                    ViewBag.NextGuid = en.Guid;
                    ViewBag.NextTitle = en.ArticleTitle;
                }
            }

            if (entity == null || entity.Keywords.IsNullOrWhiteSpace() || entity.Description.IsNullOrWhiteSpace())
            {
                var configEn = _sysConfigService.FindByClause(o => o.ConfigCode == "baseConfig");
                if (configEn != null)
                {
                    if (!configEn.ConfigContent.IsNullOrWhiteSpace())
                    {
                        var seoEn = JsonHelper.DeserializeObject<SysConfigRequestModel>(configEn.ConfigContent);

                        entity.Keywords = seoEn.Keywords;
                        entity.Description = seoEn.Description;
                    }
                }
            }

            ViewBag.Title = entity.ArticleTitle;
            ViewBag.Keywords = entity.Keywords;
            ViewBag.Description = entity.Description;

            ViewBag.RecommendList = _articleService.FindListByClause(o => o.IsRecommend == true && o.Status == StatusEnum.Effective && o.PulishStatus == 1, o => o.CreateTime, OrderByType.Desc);
            ViewBag.HotList = _articleService.FindHotList();
            ViewBag.MenuList = _articleCategoryService.FindListByClause(o => o.Status == StatusEnum.Effective, o => o.Sort, OrderByType.Asc);


            return View("Detail", entity);
        }

        private string GeneratePages(int pageIndex, int pageSize, string url, int total)
        {
            int totalBut = 10;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div><ul>");

            int totalPage = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;

            if (totalPage > totalBut)
            {
                //以当前页为中点 向左递增3页做为启始页
                int start = pageIndex - 3 > 1 ? pageIndex - 3 : 2;
                //以当前页为中点 向右递增3页做为结束页
                int end = pageIndex + 3 > totalPage - 1 ? totalPage - 1 : pageIndex + 3;

                //当pageIndex太靠左或右时 补齐
                int shortOf = ((totalBut - 3) - (end - start) - 1);
                if (shortOf > 0)
                {
                    if (start < totalBut / 2)
                    {
                        end = end + shortOf;
                        if (end > totalPage - 1) end = totalPage - 1;
                    }
                    else
                    {
                        start = start - shortOf;
                        if (start < 2) start = 2;
                    }
                }
                //第一页
                if (pageIndex == 1)
                {
                    sb.Append($"<li><a class='active'>1</a></li>");
                }
                else
                {
                    sb.Append($"<li><a href='{url}1'>1</a></li>");
                }
                //不是第2页显示...
                if (start != 2)
                {
                    if (start - 1 == 2)
                    {
                        sb.Append($"<li><a href='{url}{start - 1}'>{start - 1}</a></li>");
                    }
                    else
                    {
                        sb.Append($"<li><a href='{url}{start - 1}'>...</a></li>");
                    }
                }
                //循环显示页
                for (int i = start; i <= end; i++)
                {
                    if (pageIndex == i)
                    {
                        sb.Append($"<li><a class='active'>{i}</a></li>");
                    }
                    else
                    {
                        sb.Append($"<li><a href='{url}{i}'>{i}</a></li>");
                    }
                }
                //不是倒数第2页显示...
                if (end != totalPage - 1)
                {
                    if (end + 1 == totalPage - 1)
                    {
                        sb.Append($"<li><a href='{url}{end + 1}'>{end + 1}</a></li>");
                    }
                    else
                    {
                        sb.Append($"<li><a href='{url}{end + 1}'>...</a></li>");
                    }
                }
                //最后一页
                if (pageIndex == totalPage)
                {
                    sb.Append($"<li><a class='active'>{totalPage}</a></li>");
                }
                else
                {
                    sb.Append($"<li><a href='{url}{totalPage}'>{totalPage}</a></li>");
                }
            }
            else
            {
                for (int i = 1; i <= totalPage; i++)
                {
                    if (i != pageIndex)
                    {
                        sb.Append($"<li><a href='{url}{i}'>{i}</a></li>");
                    }
                    else
                    {
                        sb.Append($"<li><a class='active'>{i}</a></li>");
                    }
                }
            }

            sb.Append("</div></ul>");
            return sb.ToString();
        }
    }
}
