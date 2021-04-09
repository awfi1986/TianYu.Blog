using Microsoft.AspNetCore.Mvc;

namespace TianYu.Blog.WebMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}