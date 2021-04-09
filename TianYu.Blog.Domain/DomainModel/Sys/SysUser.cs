
using SqlSugar; 
using TianYu.Blog.Infrastructure.Enums;

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [SugarTable("sys_user")]
    public class SysUser : AggregateRoot
    {
        /// <summary>
        /// guid
        /// </summary>
        [SugarColumn(ColumnName = "guid", IsNullable = false, IsPrimaryKey = true, IsIdentity = false)]
        public string Guid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [SugarColumn(ColumnName = "user_pwd")]
        public string UserPwd { get; set; }
        /// <summary>
        /// 盐值
        /// </summary>
        [SugarColumn(ColumnName = "salt_value")]
        public string SaltValue { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnName = "true_name")]
        public string TrueName { get; set; }
        /// <summary>
        /// 性别（0＝不详；1＝男；2＝女）
        /// </summary>
        [SugarColumn(ColumnName = "gender")]
        public int Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnName = "head_img")]
        public string HeadImg { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(ColumnName = "enabled")]
        public EnabledEnum Enabled { get; set; } 
    }
}
