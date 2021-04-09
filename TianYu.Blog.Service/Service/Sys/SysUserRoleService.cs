using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public class SysUserRoleService : BaseRepository<SysUserRole>, ISysUserRoleService
    { 
        public SysUserRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
