using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using TianYu.Core.Common;

namespace TianYu.Core.DataBase
{
  public static class SqlSugarService
    {
        public static void AddSqlSugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 默认添加主数据库连接
            MainDb.CurrentDbConnId = AppsettingsHelper.app(new string[] { "MainDB" });

            // 把多个连接对象注入服务，这里必须采用Scope，因为有事务操作
            services.AddScoped<ISqlSugarClient>(o =>
            {
                var listConfig = new List<ConnectionConfig>();

                Config.MutiConnectionString.ForEach(m =>
                {
                    listConfig.Add(new ConnectionConfig()
                    {
                        ConfigId = m.ConnId.ObjToString().ToLower(),
                        ConnectionString = m.Conn,
                        DbType = (DbType)m.DbType,
                        IsAutoCloseConnection = true,
                        IsShardSameThread = false,
                        AopEvents = new AopEvents
                        {
                            OnLogExecuting = (sql, p) =>
                            {
                                // 多库操作的话，此处暂时无效果，在另一个地方有效，具体请查看BaseRepository.cs
                            }
                        },
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    }
                   );
                });
                return new SqlSugarClient(listConfig);
            });
        }
    }

    public static class MainDb
    {
        public static string CurrentDbConnId = "1";
    }
}
