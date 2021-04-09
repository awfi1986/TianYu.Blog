
using TianYu.Core.Common.BaseViewModel;
using TianYu.Blog.Infrastructure.Enums;

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class SysRoleListRequestModel : BaseRequest
    {
        public int RoleId { get; set; }
    }
    public class SaveSysRoleRequestModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary> 
        public string RoleName { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary> 
        public string RoleCode { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary> 
        public string RoleDesc { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public string Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary> 
        public bool Enabled { get; set; }
    }
}
