using SqlSugar;

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 文章
    /// </summary>
    [SugarTable("article")]
    public class Article : AggregateRoot
    {
        /// <summary>
        /// guid
        /// </summary>
        [SugarColumn(ColumnName = "guid", IsNullable = false, IsPrimaryKey = true)]
        public string Guid { get; set; } 
        /// <summary>
        /// 分类Id
        /// </summary>
        [SugarColumn(ColumnName = "category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// keywords
        /// </summary>
        [SugarColumn(ColumnName = "keywords")]
        public string Keywords { get; set; }
        /// <summary>
        /// description
        /// </summary>
        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        [SugarColumn(ColumnName = "article_title")]
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 标题图片
        /// </summary>
        [SugarColumn(ColumnName = "title_img")]
        public string TitleImg { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        [SugarColumn(ColumnName = "subtitle")]
        public string SubTitle { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        [SugarColumn(ColumnName = "content")]
        public string Content { get; set; }
        /// <summary>
        /// 发布状态（0＝未发布，1＝已发布）
        /// </summary>
        [SugarColumn(ColumnName = "pulish_status")]
        public int PulishStatus { get; set; }
        /// <summary>
        /// 是否推荐（0＝否；1＝是）
        /// </summary>
        [SugarColumn(ColumnName = "is_recommend")]
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否置顶（0＝否；1＝是）
        /// </summary>
        [SugarColumn(ColumnName = "is_top")]
        public bool IsTop { get; set; }
        /// <summary>
        /// 是否原创（0＝否；1＝是）
        /// </summary>
        [SugarColumn(ColumnName = "is_original")]
        public bool IsOriginal { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        [SugarColumn(ColumnName = "pv_count")]
        public int PVCount { get; set; } 
    }
}
