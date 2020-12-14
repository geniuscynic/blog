using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DbType = SqlSugar.DbType;

namespace Blog.Common.Extensions.ServiceExtensions
{
    public static class SqlsugarSetup
    {
        public static int i = 1;
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {

            services.AddScoped<IDbConnection>(o =>
            {
                       var connection = new SqlConnection(AppSettingHelper.App("ConnectionStrings:Default"));

                       return connection;
            });

            services.AddScoped<ISqlSugarClient>(o =>
            {
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = AppSettingHelper.App("ConnectionStrings:Default"),//连接符字串
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息

                    // 自定义特性
                    ConfigureExternalServices = new ConfigureExternalServices()
                    {
                        EntityService = (property, column) =>
                        {
                            if (column.IsPrimarykey && property.PropertyType == typeof(int))
                            {
                                column.IsIdentity = true;
                            }
                        }
                    }
                });

                //添加Sql打印事件，开发中可以删掉这个代码
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    var res = sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                    MiniProfiler.Current.Step($"执行的sql: {++i} \r\n {res}");
                    //MiniProfiler.Current.Step($"{res}");

                    Console.WriteLine($"生成的sql {i}:\r\n");
                    Console.WriteLine(res);
                    Console.WriteLine();
                };


                return db;
            });
        }
    }
}
