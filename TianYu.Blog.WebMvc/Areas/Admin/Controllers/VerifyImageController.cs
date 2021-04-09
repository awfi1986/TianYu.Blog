using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VerifyImageController : Controller
    {
        public FileContentResult Index()
        {
            string code = string.Empty;

            var stream = Utils.CreateVerifyImage(out code);

            HttpContext.Session.SetString("Captcha", code);

            return File(stream.ToArray(), "image/gif");

        }
    }
}