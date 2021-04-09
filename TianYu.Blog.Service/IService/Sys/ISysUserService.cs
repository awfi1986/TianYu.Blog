
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;

namespace TianYu.Blog.Service
{
    public interface ISysUserService : IBaseRepository<SysUser>
    {
        Task<List<SysUser>> FindPageListAsync(string keyWords, int roleId, int pageIndex, int pageSize, RefAsync<int> total); 
    }
}
