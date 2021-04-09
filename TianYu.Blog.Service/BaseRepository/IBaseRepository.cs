using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TianYu.Blog.Service
{
    public interface IBaseRepository<T> where T : class
    {
        #region 添加
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Insert(T entity);
        int Insert(List<T> entityList);
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        void InsertQueue(T entity); 
        void InsertQueue(List<T> entityList);
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<long> InsertAsync(T entity); 
        Task<long> InsertAsync(List<T> entityList); 
        Task<bool> InsertIdentityIntoEntityAsync(T entity); 
        Task<bool> InsertIdentityIntoEntityAsync(List<T> entityList);
        #endregion

        #region 修改
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);
        int Update(List<T> updateObjs);
        int Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void UpdateQueue(T entity); 
        void UpdateQueue(T[] updateObjs); 
        void UpdateQueue(List<T> updateObjs); 
        void UpdateQueue(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity); 
        Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression); 
        Task<bool> UpdateAsync(T[] updateObjs); 
        Task<bool> UpdateAsync(List<T> updateObjs);

        #endregion

        #region 删除
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Delete(T entity);
        int Delete(Expression<Func<T, bool>> whereExpression);
        int DeleteById(object id);
        int DeleteByIds(object[] ids);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        void DeleteQueue(T entity); 
        void DeleteQueue(Expression<Func<T, bool>> whereExpression); 
        void DeleteByIdQueue(object id); 
        void DeleteByIdsQueue(object[] ids);
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity); 
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression); 
        Task<bool> DeleteByIdAsync(object id); 
        Task<bool> DeleteByIdsAsync(object[] ids);
        #endregion 

        #region 普通查询 
        bool Any(Expression<Func<T, bool>> whereExpression);
        bool Any<T1>(Expression<Func<T, bool>> whereExpression);
        int Count(Expression<Func<T, bool>> whereExpression);
        T FindById(object pkValue);
        T FindById(Expression<Func<T, T>> selectExpression, object pkValue);
        T FindSingle(Expression<Func<T, bool>> whereExpression);
        T FindSingle(Expression<Func<T, T>> selectExpressio, Expression<Func<T, bool>> whereExpression);
        T FindByClause(Expression<Func<T, bool>> whereExpression);
        T FindByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression);
        IEnumerable<T> FindListByClause(Expression<Func<T, bool>> whereExpression);
        IEnumerable<T> FindListByClause(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type);
        IEnumerable<T> FindListByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression);
        IEnumerable<T> FindListByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type);


        Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> AnyAsync<T1>(Expression<Func<T, bool>> whereExpression);
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);
        Task<T> FindByIdAsync(object pkValue);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> whereExpression);
        Task<T> FindByClauseAsync(Expression<Func<T, bool>> whereExpression);
        Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> whereExpression);
        Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type);
        Task<IEnumerable<T>> FindListByClauseAsync(List<IConditionalModel> conditionalModels);
        Task<IEnumerable<T>> FindListByClauseAsync(List<IConditionalModel> conditionalModels, Expression<Func<T, object>> orderByExpression, OrderByType type);

        #endregion

        #region 分页查询
        List<T> FindPageList(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, ref int count);
        List<T> FindPageList(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, string orderBy, ref int count);
        Task<List<T>> FindPageListAsync(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, RefAsync<int> count);
        Task<List<T>> FindPageListAsync(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, string orderBy, RefAsync<int> count);
        #endregion

        #region 多表查询
        ISugarQueryable<T1, T2> Find<T1, T2>(Expression<Func<T1, T2, object[]>> joinExpression);
        ISugarQueryable<T1, T2> Find<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression);
        ISugarQueryable<T1, T2, T3> Find<T1, T2, T3>(Expression<Func<T1, T2, T3, object[]>> joinExpression);
        ISugarQueryable<T1, T2, T3> Find<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4> Find<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object[]>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4> Find<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4, T5> Find<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object[]>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4, T5> Find<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4, T5, T6> Find<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object[]>> joinExpression);
        ISugarQueryable<T1, T2, T3, T4, T5, T6> Find<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression);

        #endregion

        #region 事物
        bool SaveQueues();
        Task<int> SaveQueuesAsync(); 

        #endregion
    }
}
