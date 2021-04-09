using System;
using System.Collections.Generic;
using System.Text;

namespace TianYu.Blog.Domain.ViewModel
{
    public class SysMenuViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary> 
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary> 
        public int MenuCode { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary> 
        public string MenuIcon { get; set; }
        /// <summary>
        /// 页索引
        /// </summary> 
        public string PageTitle { get; set; }
        /// <summary>
        /// 菜单Url
        /// </summary> 
        public string MenuUrl { get; set; }
        /// <summary>
        /// 父级ID(-1为顶级)
        /// </summary> 
        public int ParentId { get; set; }
        /// <summary>
        /// 父级编码
        /// </summary> 
        public int ParentCode { get; set; } 
        /// <summary>
        /// 层级
        /// </summary> 
        public int Level { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }
    }
}
