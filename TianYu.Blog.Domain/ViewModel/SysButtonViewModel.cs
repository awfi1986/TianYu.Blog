
namespace TianYu.Blog.Domain.ViewModel
{
    public class SysButtonViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// Js事件名称
        /// </summary> 
        public string JsEvent { get; set; }
        /// <summary>
        /// 图标
        /// </summary> 
        public string Icon { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary> 
        public int MenuId { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary> 
        public string BackColor { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary> 
        public string SizeStyle { get; set; }
        /// <summary>
        /// 分组
        /// </summary> 
        public int GroupId { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public string Sort { get; set; } 
        /// <summary>
        /// (0=自定义按钮;1=工具栏按钮)
        /// </summary> 
        public int IsToolbar { get; set; }
    }
}
