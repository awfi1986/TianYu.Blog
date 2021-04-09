using SqlSugar;
using System.Collections.Generic;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Infrastructure;

namespace TianYu.Blog.Service
{
    public class SysButtonService : BaseRepository<SysButton>, ISysButtonService
    {
        public SysButtonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// 查询用户按钮
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public List<SysButton> FindUserButton(string userGuid)
        {
            return FindUserButton(userGuid, null);
        }
        public List<SysButton> FindUserButton(string userGuid, int? mid)
        {
            if (userGuid != BlogConsts.SystemSuperAdminAccount)
            {
                var list = Db.Queryable<SysButton, SysRolePower, SysUserRole>((b, p, r) =>
                         new JoinQueryInfos(
                           JoinType.Left, b.Id == p.PowerId,
                           JoinType.Left, p.RoleId == r.RoleId
                         ))
                         .Where((b, p, r) => b.Status == 0 && p.PowerType == 2 && (r.UserGuid == userGuid || userGuid == BlogConsts.SystemSuperAdminAccount))
                         .WhereIF(mid.HasValue, b => b.MenuId == mid)
                         .Select(b => b).OrderBy(b => b.Sort).ToList();
                return list;
            }
            else
            {
                var list = Db.Queryable<SysButton>().Where(b => b.Status == 0)
                    .WhereIF(mid.HasValue, b => b.MenuId == mid)
                    .Select(b => b).OrderBy(b => b.Sort).ToList();
                return list;
            }
        }
    }
}
