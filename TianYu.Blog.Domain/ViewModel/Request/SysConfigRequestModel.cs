 

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class SysConfigRequestModel
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        public string SEOTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string WxNotifyUrl { get; set; }
        public string MchId { get; set; }
        public string PayKey { get; set; }
    }
}
