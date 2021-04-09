using System;

namespace TianYu.Blog.Domain.ViewModel.Response
{
    public class ArticleListResponseModel
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
        /// 分类名称
        /// </summary>
        public string CateoryName { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary> 
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
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
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }

    public class HotArticleResponseModel
    {
        /// <summary>
        /// guid
        /// </summary> 
        public string Guid { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary> 
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 标题图片
        /// </summary> 
        public string TitleImg { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }

    public class UpNextArticleResponseModel
    {
        /// <summary>
        /// guid
        /// </summary> 
        public string Guid { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary> 
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 上下篇标示（1＝上一篇；2＝下一篇）
        /// </summary> 
        public int Tag { get; set; }
    }
}
