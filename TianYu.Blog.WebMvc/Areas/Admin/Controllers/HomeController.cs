using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TianYu.Blog.Service;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private ISysMenuService _sysMenuService; 

        public HomeController(ISysMenuService sysMenuService)
        {
            this._sysMenuService = sysMenuService; 
        }



        public IActionResult Index()
        {
            var menuList = _sysMenuService.FindUserMenu(base.LoginUserGuid);

            ViewBag.LoginName = HttpContext.Session.GetString("Admin_UserName");

            return View(menuList);
        }

        public IActionResult Main()
        {
            return View();
        }
    }
}
