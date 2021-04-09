using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class ArticleListRequestModel : BaseRequest
    {
        public int? CategoryId { get; set; }
        public int? PulishStatus { get; set; }
    }

    public class SaveArticleRequestModel
    {
        /// <summary>
        /// guid
        /// </summary> 
        public string Guid { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary> 
        public int CategoryId { get; set; }
        /// <summary>
        /// keywords
        /// </summary> 
        public string Keywords { get; set; }
        /// <summary>
        /// description
        /// </summary> 
        public string Description { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary> 
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 标题图片
        /// </summary>
        public string TitleImg { get; set; }
        /// <summary>
        /// 副标题
        /// </summary> 
        public string SubTitle { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary> 
        public string Content { get; set; }
        /// <summary>
        /// 发布状态（0＝未发布，1＝已发布）
        /// </summary> 
        public int PulishStatus { get; set; }
        /// <summary>
        /// 是否推荐（0＝否；1＝是）
        /// </summary> 
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否置顶（0＝否；1＝是）
        /// </summary> 
        public bool IsTop { get; set; }
        /// <summary>
        /// 是否原创（0＝否；1＝是）
        /// </summary> 
        public bool IsOriginal { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary> 
        public int PVCount { get; set; }
    }
}
