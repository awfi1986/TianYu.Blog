using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TianYu.Core.Common;

namespace TianYu.Blog.WebMvc.Areas.Admin.Controllers
{
    public class UploadController : BaseController
    {
        public const string fileFilt = ".gif|.jpg|.jpeg|.png";
        public const long fileSize = 1024 * 1024 * 2;//2M
        public const string direName = "UploadFile";
        public IWebHostEnvironment _webHostEnvironment;
        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }


        public async Task<JsonResult> Upload()
        {
            var res = new AjaxResult();
            res.Code = ResultCode.Failure;

            var files = Request.Form.Files;

            if (files.Count < 1)
            {
                res.Message = "没有选择上传文件";
                return Json(res);
            }
            var rootPath = _webHostEnvironment.WebRootPath;
            var resList = new List<UploadResult>();

            foreach (var formFile in files)
            {
                if (formFile != null && formFile.Length > 0)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(formFile.FileName).ToLower();

                    if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    {
                        res.Message = $"请上传{fileFilt}格式的图片";
                        return Json(res);
                    }

                    //判断文件大小     
                    if (formFile.Length > fileSize)
                    {
                        res.Message = $"上传的文件不能大于{fileSize}M";
                        return Json(res);
                    }

                    string newFileName = Utils.NewStrGuid() + fileExtension;
                    string saveDir = DateTime.Now.ToString("yyyyMM");
                    string savePath = Path.Combine(rootPath, direName, saveDir);

                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    using (var stream = System.IO.File.Create(Path.Combine(savePath, newFileName)))
                    {
                        await formFile.CopyToAsync(stream);

                        resList.Add(new UploadResult()
                        {
                            SaveFileName = newFileName,
                            SaveFilePath = Path.Combine("/", direName, saveDir, newFileName),
                            UploadFileName = formFile.FileName
                        });
                    }
                }
            }
            res.Data = resList;
            res.Code = ResultCode.Succeed;
            return Json(res);
        }
        
    }


    public class UploadResult
    {
        public string SaveFileName { get; set; }
        public string UploadFileName { get; set; }
        public string SaveFilePath { get; set; }
    }

    public enum UploadFileType
    {
        File,
        Image
    }
}