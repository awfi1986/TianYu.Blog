using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public class SysRoleService : BaseRepository<SysRole>, ISysRoleService
    { 
        public SysRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
