using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public class SysRolePowerService : BaseRepository<SysRolePower>, ISysRolePowerService
    { 
        public SysRolePowerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
