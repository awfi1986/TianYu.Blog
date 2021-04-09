using SqlSugar; 

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarTable("sys_user_role")]
    public class SysUserRole 
    {
        /// <summary>
        /// 用户Guid
        /// </summary>
        [SugarColumn(ColumnName = "user_guid")]
        public string UserGuid { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; } 
    }
}
