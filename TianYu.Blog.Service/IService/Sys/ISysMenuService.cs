
using System.Collections.Generic;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel;

namespace TianYu.Blog.Service
{
    public interface ISysMenuService : IBaseRepository<SysMenu>
    {
        /// <summary>
        /// 查询用户菜单
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        List<SysMenuViewModel> FindUserMenu(string userGuid);
    }
}
