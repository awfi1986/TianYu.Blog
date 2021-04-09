using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Infrastructure;

namespace TianYu.Blog.Service
{
    public class SysUserService : BaseRepository<SysUser>, ISysUserService
    {
        public SysUserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<List<SysUser>> FindPageListAsync(string keyWords, int roleId, int pageIndex, int pageSize, RefAsync<int> total)
        {
            var list = await Db.Queryable<SysUser, SysUserRole>((a, b) => new JoinQueryInfos(JoinType.Left, a.Guid == b.UserGuid))
                .WhereIF(!string.IsNullOrEmpty(keyWords), a => a.UserName.Contains(keyWords) || a.TrueName.Contains(keyWords))
                .WhereIF(roleId != 0, (a, b) => b.RoleId == roleId)
                .Where((a, b) => a.Status == 0 && a.Guid != BlogConsts.SystemSuperAdminAccount)
                .Select(a => a)
                .Distinct()
                .OrderBy(a => a.CreateTime, OrderByType.Desc)
                .ToPageListAsync(pageIndex, pageSize, total);

            return list;
        }
    }
}
