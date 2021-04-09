using System.ComponentModel; 

namespace TianYu.Blog.Infrastructure.Enums
{
    public enum EnabledEnum
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable,
    }
}
