
using SqlSugar; 

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统按钮
    /// </summary>
    [SugarTable("sys_button")]
    public class SysButton : AggregateRoot
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        [SugarColumn(ColumnName = "button_name")]
        public string ButtonName { get; set; }
        /// <summary>
        /// Js事件名称
        /// </summary>
        [SugarColumn(ColumnName = "js_event")]
        public string JsEvent { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [SugarColumn(ColumnName = "icon")]
        public string Icon { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        [SugarColumn(ColumnName = "menu_id")]
        public int MenuId { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        [SugarColumn(ColumnName = "back_color")]
        public string BackColor { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        [SugarColumn(ColumnName = "size_style")]
        public string SizeStyle { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        [SugarColumn(ColumnName = "group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public int Sort { get; set; }  
        /// <summary>
        /// (0=自定义按钮;1=工具栏按钮)
        /// </summary>
        [SugarColumn(ColumnName = "is_toolbar")]
        public int IsToolbar { get; set; }
    }
}
