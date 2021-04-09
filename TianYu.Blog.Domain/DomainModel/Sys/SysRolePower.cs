using SqlSugar;

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [SugarTable("sys_role_power")]
    public class SysRolePower 
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        [SugarColumn(ColumnName = "power_id")]
        public int PowerId { get; set; }
        /// <summary>
        /// 权限类型（1＝菜单；2＝按钮）
        /// </summary>
        [SugarColumn(ColumnName = "power_type")]
        public int PowerType { get; set; } 
    }
}
