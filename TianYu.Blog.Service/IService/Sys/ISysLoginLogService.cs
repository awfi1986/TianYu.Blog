
//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2021-01-18 15:06:23 
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;

namespace TianYu.Blog.Service
{
    public interface ISysLoginLogService : IBaseRepository<SysLoginLog>
    {
        Task<List<SysLoginLog>> FindPageListAsync(string keyWords, int pageIndex, int pageSize, RefAsync<int> total);
    }
}
	 
