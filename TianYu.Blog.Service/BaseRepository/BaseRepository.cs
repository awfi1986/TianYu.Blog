using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TianYu.Core.Common;

namespace TianYu.Blog.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private SqlSugarClient _dbBase;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbBase = unitOfWork.GetDbClient();
        }

        private ISqlSugarClient _db
        {
            get
            {
                /* 如果要开启多库支持，
                 * 1、在appsettings.json 中开启MutiDBEnabled节点为true，必填
                 * 2、设置一个主连接的数据库ID，节点MainDB，对应的连接字符串的Enabled也必须true，必填
                 */
                if (AppsettingsHelper.app(new string[] { "MutiDBEnabled" }).ObjToBool())
                {
                    if (typeof(T).GetTypeInfo().GetCustomAttributes(typeof(SugarTable), true).FirstOrDefault((x => x.GetType() == typeof(SugarTable))) is SugarTable sugarTable && !string.IsNullOrEmpty(sugarTable.TableDescription))
                    {
                        _dbBase.ChangeDatabase(sugarTable.TableDescription.ToLower());
                    }
                    else
                    {
                        //_dbBase.ChangeDatabase(MainDb.CurrentDbConnId.ToLower());
                    }
                }

                if (AppsettingsHelper.app(new string[] { "AppSettings", "SqlAOP", "Enabled" }).ObjToBool())
                {
                    _dbBase.Aop.OnLogExecuting = (sql, pars) => //SQL执行中事件
                    {
                        Parallel.For(0, 1, e =>
                        {
                            //MiniProfiler.Current.CustomTiming("SQL：", GetParas(pars) + "【SQL语句】：" + sql);
                            //LogLock.OutSql2Log("SqlLog", new string[] { GetParas(pars), "【SQL语句】：" + sql });

                        });
                    };
                }

                return _dbBase;
            }
        }

        internal ISqlSugarClient Db
        {
            get { return _db; }
        }

        #region 添加
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            return Db.Insertable(entity).ExecuteCommand();
        }
        public int Insert(List<T> entityList)
        {
            return Db.Insertable(entityList).ExecuteCommand();
        }
        /// <summary>
        /// 事务添加数据
        /// </summary>
        /// <param name="entity"></param>
        public void InsertQueue(T entity)
        {
            Db.Insertable(entity).AddQueue();
        }
        public void InsertQueue(List<T> entityList)
        {
            Db.Insertable(entityList).AddQueue();
        }
        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<long> InsertAsync(T entity)
        {
            return await Db.Insertable(entity).ExecuteReturnBigIdentityAsync();
        }
        public async Task<long> InsertAsync(List<T> entityList)
        {
            return await Db.Insertable(entityList).ExecuteReturnBigIdentityAsync();
        }
        public async Task<bool> InsertIdentityIntoEntityAsync(T entity)
        {
            return await Db.Insertable(entity).ExecuteCommandIdentityIntoEntityAsync();
        }
        public async Task<bool> InsertIdentityIntoEntityAsync(List<T> entityList)
        {
            return await Db.Insertable(entityList).ExecuteCommandIdentityIntoEntityAsync();
        }
        #endregion

        #region 修改 
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return Db.Updateable(entity).ExecuteCommand();
        }
        public int Update(List<T> updateObjs)
        {
            return Db.Updateable(updateObjs).ExecuteCommand();
        }
        public int Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return Db.Updateable<T>().SetColumns(columns).Where(whereExpression).ExecuteCommand();
        }

        /// <summary>
        /// 事务修改数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQueue(T entity)
        {
            //这种方式会以主键为条件
            Db.Updateable(entity).AddQueue();
        }
        public void UpdateQueue(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            Db.Updateable<T>().SetColumns(columns).Where(whereExpression).AddQueue();
        }
        public void UpdateQueue(T[] updateObjs)
        {
            Db.Updateable(updateObjs).AddQueue();
        }
        public void UpdateQueue(List<T> updateObjs)
        {
            Db.Updateable(updateObjs).AddQueue();
        }
        /// <summary>
        /// 异步修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            //这种方式会以主键为条件
            return await Db.Updateable(entity).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Updateable<T>().SetColumns(columns).Where(whereExpression).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> UpdateAsync(T[] updateObjs)
        {
            return await Db.Updateable(updateObjs).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> UpdateAsync(List<T> updateObjs)
        {
            return await Db.Updateable(updateObjs).ExecuteCommandAsync() > 0;
        }
        #endregion

        #region 删除 
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            return Db.Deleteable(entity).ExecuteCommand();
        }
        public int Delete(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Deleteable<T>(whereExpression).ExecuteCommand();
        }
        public int DeleteById(object id)
        {
            return Db.Deleteable<T>(id).ExecuteCommand();
        }
        public int DeleteByIds(object[] ids)
        {
            return Db.Deleteable<T>().In(ids).ExecuteCommand();
        }
        /// <summary>
        /// 事务删除数据
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteQueue(T entity)
        {
            Db.Deleteable(entity).AddQueue();
        }
        public void DeleteQueue(Expression<Func<T, bool>> whereExpression)
        {
            Db.Deleteable<T>(whereExpression).AddQueue();
        }
        public void DeleteByIdQueue(object id)
        {
            Db.Deleteable<T>(id).AddQueue();
        }
        public void DeleteByIdsQueue(object[] ids)
        {
            Db.Deleteable<T>().In(ids).AddQueue();
        }
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            return await Db.Deleteable(entity).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Deleteable<T>(whereExpression).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> DeleteByIdAsync(object id)
        {
            return await Db.Deleteable<T>(id).ExecuteCommandAsync() > 0;
        }
        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            return await Db.Deleteable<T>().In(ids).ExecuteCommandAsync() > 0;
        }

        #endregion 

        #region 普通查询
        public bool Any(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Where(whereExpression).Any();
        }
        public bool Any<T1>(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Where(whereExpression).Any();
        }
        public int Count(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Where(whereExpression).Count();
        }
        public T FindById(object pkValue)
        {
            return Db.Queryable<T>().InSingle(pkValue);
        }
        public T FindById(Expression<Func<T, T>> selectExpression, object pkValue)
        {
            return Db.Queryable<T>().Select(selectExpression).InSingle(pkValue);
        }
        public T FindSingle(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Single(whereExpression);
        }
        public T FindSingle(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Select(selectExpression).Single(whereExpression);
        }
        public T FindByClause(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().First(whereExpression);
        }
        public T FindByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Select(selectExpression).First(whereExpression);
        }
        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Where(whereExpression).ToList();
        }
        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type)
        {
            return Db.Queryable<T>().Where(whereExpression).OrderBy(orderByExpression, type).ToList();
        }
        public IEnumerable<T> FindListByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression)
        {
            return Db.Queryable<T>().Where(whereExpression).Select(selectExpression).ToList();
        }
        public IEnumerable<T> FindListByClause(Expression<Func<T, T>> selectExpression, Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type)
        {
            return Db.Queryable<T>().Where(whereExpression).Select(selectExpression).OrderBy(orderByExpression, type).ToList();
        }


        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).AnyAsync();
        }
        public async Task<bool> AnyAsync<T1>(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).AnyAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).CountAsync();
        }
        public async Task<T> FindByIdAsync(object pkValue)
        {
            return await Db.Queryable<T>().InSingleAsync(pkValue);
        }
        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().SingleAsync(whereExpression);
        }
        public async Task<T> FindByClauseAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().FirstAsync(whereExpression);
        }
        public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, OrderByType type)
        {
            return await Db.Queryable<T>().Where(whereExpression).OrderBy(orderByExpression, type).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListByClauseAsync(List<IConditionalModel> conditionalModels)
        {
            return await Db.Queryable<T>().Where(conditionalModels).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListByClauseAsync(List<IConditionalModel> conditionalModels, Expression<Func<T, object>> orderByExpression, OrderByType type)
        {
            return await Db.Queryable<T>().Where(conditionalModels).OrderBy(orderByExpression, type).ToListAsync();
        }

        #endregion

        #region 分页查询
        public List<T> FindPageList(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, ref int count)
        {
            return Db.Queryable<T>().Where(whereExpression).ToPageList(pageIndex, pageSize, ref count);
        }
        public List<T> FindPageList(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, string orderBy, ref int count)
        {
            var query = Db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            var result = query.ToPageList(pageIndex, pageSize, ref count);
            return result;
        }
        public async Task<List<T>> FindPageListAsync(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, RefAsync<int> count)
        {
            return await Db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToPageListAsync(pageIndex, pageSize, count);
        }
        public async Task<List<T>> FindPageListAsync(Expression<Func<T, bool>> whereExpression, int pageIndex, int pageSize, string orderBy, RefAsync<int> count)
        {
            var query = Db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            var result = await query.ToPageListAsync(pageIndex, pageSize, count);
            return result;
        }
        #endregion

        #region 多表查询
        public ISugarQueryable<T1, T2> Find<T1, T2>(Expression<Func<T1, T2, object[]>> joinExpression)
        {
            return Db.Queryable<T1, T2>(joinExpression);
        }
        public ISugarQueryable<T1, T2> Find<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression)
        {
            return Db.Queryable<T1, T2>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3> Find<T1, T2, T3>(Expression<Func<T1, T2, T3, object[]>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3> Find<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4> Find<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object[]>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4> Find<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4, T5> Find<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object[]>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4, T5>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4, T5> Find<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4, T5>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4, T5, T6> Find<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object[]>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4, T5, T6>(joinExpression);
        }
        public ISugarQueryable<T1, T2, T3, T4, T5, T6> Find<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression)
        {
            return Db.Queryable<T1, T2, T3, T4, T5, T6>(joinExpression);
        }

        #endregion

        #region 事物 
        public bool SaveQueues()
        {
            return Db.SaveQueues() > 0;
        }
        public Task<int> SaveQueuesAsync()
        {
            var ar = Db.SaveQueuesAsync();
            ar.Wait();

            return ar;
        } 

        #endregion
    }
}
