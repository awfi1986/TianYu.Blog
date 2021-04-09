using SqlSugar;
using System.Collections.Generic;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel;
using TianYu.Blog.Infrastructure;

namespace TianYu.Blog.Service
{
    public class SysMenuService : BaseRepository<SysMenu>, ISysMenuService
    {
        public SysMenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// 查询用户菜单
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public List<SysMenuViewModel> FindUserMenu(string userGuid)
        {
            if (userGuid != BlogConsts.SystemSuperAdminAccount)
            {
                var list = Db.Queryable<SysMenu, SysRolePower, SysUserRole>((m, p, r) =>
                             new JoinQueryInfos(
                               JoinType.Left, m.Id == p.PowerId,
                                JoinType.Left, p.RoleId == r.RoleId
                             ))
                             .Where((m, p, r) => m.Status == 0 && m.Enabled == true && p.PowerType == 1 && r.UserGuid == userGuid)
                             .Select((m, p, r) => new SysMenuViewModel
                             {
                                 Id = m.Id,
                                 Level = m.Level,
                                 MenuCode = m.MenuCode,
                                 MenuIcon = m.MenuIcon,
                                 MenuName = m.MenuName,
                                 MenuUrl = m.MenuUrl,
                                 ParentId = m.ParentId,
                                 Sort = m.Sort
                             }).OrderBy(m => m.Sort).ToList();

                return list;
            }
            else
            {
                var list = Db.Queryable<SysMenu>()
                             .Where(m => m.Status == 0 && m.Enabled == true)
                             .Select(m => new SysMenuViewModel
                             {
                                 Id = m.Id,
                                 Level = m.Level,
                                 MenuCode = m.MenuCode,
                                 MenuIcon = m.MenuIcon,
                                 MenuName = m.MenuName,
                                 MenuUrl = m.MenuUrl,
                                 ParentId = m.ParentId,
                                 Sort = m.Sort
                             }).OrderBy(m => m.Sort).ToList();

                return list;
            }
        }
    }
}
