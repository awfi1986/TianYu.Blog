using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using TianYu.Blog.Domain.DomainModel;
using TianYu.Blog.Domain.ViewModel.Request;
using TianYu.Blog.Domain.ViewModel.Response;
using TianYu.Blog.Infrastructure.Enums;

namespace TianYu.Blog.Service
{
    public class ArticleService : BaseRepository<Article>, IArticleService
    {
        public ArticleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<ArticleListResponseModel> FindPageList(string articleTitle, int categoryId, int pageIndex, int pageSize, ref int total)
        {
            var list = Db.Queryable<Article>()
                .Where(a => a.Status == StatusEnum.Effective && a.PulishStatus == 1)
                .WhereIF(!string.IsNullOrEmpty(articleTitle), a => a.ArticleTitle.Contains(articleTitle))
                .WhereIF(categoryId > 0, a => a.CategoryId == categoryId)
                .OrderBy(a => new { a.IsTop, a.CreateTime }, OrderByType.Desc)
                .Select(a => new ArticleListResponseModel()
                {
                    Guid = a.Guid,
                    ArticleTitle = a.ArticleTitle,
                    SubTitle = a.SubTitle,
                    IsOriginal = a.IsOriginal,
                    PVCount = a.PVCount,
                    CreateTime = a.CreateTime
                })
                .ToPageList(pageIndex, pageSize, ref total);

            return list;
        }

        public async Task<List<ArticleListResponseModel>> FindPageListAsync(ArticleListRequestModel requestModel)
        {
            var list = await Db.Queryable<Article, ArticleCategory>((a, b) => new JoinQueryInfos(JoinType.Left, a.CategoryId == b.Id))
                .Where(a => a.Status == StatusEnum.Effective)
                .WhereIF(!string.IsNullOrEmpty(requestModel.KeyWords), a => a.ArticleTitle.Contains(requestModel.KeyWords))
                .WhereIF(requestModel.PulishStatus.HasValue, a => a.PulishStatus == requestModel.PulishStatus)
                .WhereIF(requestModel.CategoryId.HasValue, a => a.CategoryId == requestModel.CategoryId)
                .OrderBy(a => a.CreateTime, OrderByType.Desc)
                .Select((a, b) => new ArticleListResponseModel()
                {
                    Guid = a.Guid,
                    ArticleTitle = a.ArticleTitle,
                    CategoryId = a.CategoryId,
                    CateoryName = b.Name,
                    IsRecommend = a.IsRecommend,
                    IsTop = a.IsTop, 
                    PulishStatus = a.PulishStatus,
                    PVCount = a.PVCount,
                    CreateTime = a.CreateTime,
                    ModifyTime = a.ModifyTime
                })
                .ToPageListAsync(requestModel.Page, requestModel.Limit, requestModel.Total);

            return list;
        }

        public List<HotArticleResponseModel> FindHotList()
        {
            var list = Db.Queryable<Article>()
                .Where(a => a.Status == StatusEnum.Effective && a.PulishStatus == 1)
                .OrderBy(a => a.PVCount, OrderByType.Desc)
                .Select(a => new HotArticleResponseModel()
                {
                    Guid = a.Guid,
                    ArticleTitle = a.ArticleTitle,
                    TitleImg = a.TitleImg,
                    CreateTime = a.CreateTime
                })
                .Take(10).ToList();

            return list;
        }

        public List<UpNextArticleResponseModel> FindUpNext(string guid)
        {
            var upQuery = Db.Queryable<Article, Article>((a, b) => new JoinQueryInfos(JoinType.Left, a.CategoryId == b.CategoryId))
                .Where((a, b) => a.Status == StatusEnum.Effective && a.PulishStatus == 1 && b.Guid == guid && a.CreateTime < b.CreateTime)
                .OrderBy(a => a.CreateTime, OrderByType.Desc)
                .Select(a => new UpNextArticleResponseModel { Guid = a.Guid, ArticleTitle = a.ArticleTitle, Tag = 1 })
                .Take(1);


            var nextQuery = Db.Queryable<Article, Article>((a, b) => new JoinQueryInfos(JoinType.Left, a.CategoryId == b.CategoryId))
               .Where((a, b) => a.Status == StatusEnum.Effective && a.PulishStatus == 1 && b.Guid == guid && a.CreateTime > b.CreateTime)
               .OrderBy(a => a.CreateTime, OrderByType.Asc)
               .Select(a => new UpNextArticleResponseModel { Guid = a.Guid, ArticleTitle = a.ArticleTitle, Tag = 2 })
               .Take(1);

            var list = Db.Union(upQuery, nextQuery).ToList();

            return list;
        }
    }
}
