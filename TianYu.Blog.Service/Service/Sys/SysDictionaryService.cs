using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;

namespace TianYu.Blog.Service
{
    public class SysDictionaryService : BaseRepository<SysDictionary>, ISysDictionaryService
    {
        public SysDictionaryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<SysDictionary>> FindPageListAsync(QuerySysDictionaryRequestModel requestModel, RefAsync<int> count)
        {
            var list = await Db.Queryable<SysDictionary>()
                .Where(a => a.Status == 0 && a.ParentId == requestModel.ParentId)
                .WhereIF(!string.IsNullOrEmpty(requestModel.KeyWords), a => a.DictionaryName.Contains(requestModel.KeyWords) || a.DictionaryCode.Contains(requestModel.KeyWords))
                .Select(a => a)
                .OrderBy(a =>new { a.Sort, OrderByType.Asc, a.CreateTime, OrderByType.Desc })
                .ToPageListAsync(requestModel.Page, requestModel.Limit, count);

            return list;
        }
    }
}
