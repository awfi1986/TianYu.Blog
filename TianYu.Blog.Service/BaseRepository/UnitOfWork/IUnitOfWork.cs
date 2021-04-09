using SqlSugar;

namespace TianYu.Blog.Service
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetDbClient();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}
