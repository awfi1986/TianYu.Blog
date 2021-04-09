using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public class SysConfigService : BaseRepository<SysConfig>, ISysConfigService
    { 
        public SysConfigService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
