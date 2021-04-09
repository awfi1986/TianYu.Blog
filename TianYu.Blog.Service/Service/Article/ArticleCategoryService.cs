using TianYu.Blog.Domain.DomainModel; 

namespace TianYu.Blog.Service
{
    public class ArticleCategoryService : BaseRepository<ArticleCategory>, IArticleCategoryService
    { 
        public ArticleCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
