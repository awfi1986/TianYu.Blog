
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Domain.ViewModel.Response;

namespace TianYu.Blog.Service
{
    public interface IArticleService : IBaseRepository<Article>
    {
        Task<List<ArticleListResponseModel>>  FindPageListAsync(ArticleListRequestModel requestModel);
        List<ArticleListResponseModel> FindPageList(string articleTitle, int categoryId, int pageIndex, int pageSize, ref int total);
        List<HotArticleResponseModel> FindHotList();
        List<UpNextArticleResponseModel> FindUpNext(string guid);
    }
}
