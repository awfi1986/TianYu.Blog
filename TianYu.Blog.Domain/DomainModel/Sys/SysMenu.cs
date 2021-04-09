using SqlSugar; 

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [SugarTable("sys_menu")]
    public class SysMenu : AggregateRoot
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName="id",IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(ColumnName = "menu_name")]
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        [SugarColumn(ColumnName = "menu_code")]
        public int MenuCode { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [SugarColumn(ColumnName = "menu_icon")]
        public string MenuIcon { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        [SugarColumn(ColumnName = "page_title")]
        public string PageTitle { get; set; }
        /// <summary>
        /// 菜单Url
        /// </summary>
        [SugarColumn(ColumnName = "menu_url")]
        public string MenuUrl { get; set; }
        /// <summary>
        /// 父级ID(-1为顶级)
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 父级编码
        /// </summary>
        [SugarColumn(ColumnName = "parent_code")]
        public int ParentCode { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(ColumnName = "enabled")]
        public bool Enabled { get; set; }
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
