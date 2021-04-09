
using TianYu.Core.Common.BaseViewModel;  

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class SysUserListRequestModel : BaseRequest
    {
        public int RoleId { get; set; }
    }

    public class SysUserSaveRequestModel
    {
        public string TrueName { get; set; }
        public string OldUserPwd { get; set; }
        public string UserPwd { get; set; }
    }
    public class SaveSysUserRequestModel
    {
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string UserPwd { get; set; }
        public string HeadImg { get; set; }
        public int Gender { get; set; }
        public int Enabled { get; set; }
        public string UserRoleList { get; set; }
    }
}
