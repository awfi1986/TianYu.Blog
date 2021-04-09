
using SqlSugar; 

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [SugarTable("sys_role")]
    public class SysRole : AggregateRoot
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(ColumnName = "role_name")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [SugarColumn(ColumnName = "role_code")]
        public string RoleCode { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [SugarColumn(ColumnName = "role_desc")]
        public string RoleDesc { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public int Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [SugarColumn(ColumnName = "enabled")]
        public bool Enabled { get; set; } 
    }
}
