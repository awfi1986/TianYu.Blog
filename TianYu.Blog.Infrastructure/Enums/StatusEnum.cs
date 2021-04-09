using System.ComponentModel; 

namespace TianYu.Blog.Infrastructure.Enums
{
    public enum StatusEnum
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Effective,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid,
    }
}
