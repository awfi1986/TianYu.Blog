
using SqlSugar;

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [SugarTable("article_category")]
    public class ArticleCategory : AggregateRoot
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 父类Id
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        [SugarColumn(ColumnName = "level")]
        public int Level { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public int Sort { get; set; }
    }
}
