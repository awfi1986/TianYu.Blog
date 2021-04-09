
using System.Collections.Generic;
using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public interface ISysButtonService : IBaseRepository<SysButton>
    {
        /// <summary>
        /// 查询用户按钮
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        List<SysButton> FindUserButton(string userGuid);
        /// <summary>
        /// 查询用户按钮
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        List<SysButton> FindUserButton(string userGuid, int? mid);
    }
}
