
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;

namespace TianYu.Blog.Service
{
    public interface ISysDictionaryService : IBaseRepository<SysDictionary>
    {
        Task<List<SysDictionary>> FindPageListAsync(QuerySysDictionaryRequestModel requestModel, RefAsync<int> count);
    }
}
