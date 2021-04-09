using TianYu.Blog.Domain.DomainModel;
using TianYu.Core.Common;

namespace TianYu.Blog.Domain.ViewModel.Response
{
    public class SysUserResponseModel : SysUser
    {
        public string EnabledName { get { return Enabled.GetEnumDescription(); } }

        public string RoleName { get; set; }
    }
}
