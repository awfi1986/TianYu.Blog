using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TianYu.Blog.Service;

namespace TianYu.Blog.WebMvc.Controllers
{
    public class BaseController : Controller
    {
          

        public void GetMeunList(IArticleCategoryService articleCategoryService)
        {
            var menuList = articleCategoryService.FindListByClause(o => o.Status == Infrastructure.Enums.StatusEnum.Effective);

            ViewBag.MenuList = menuList;
        }
    }
}