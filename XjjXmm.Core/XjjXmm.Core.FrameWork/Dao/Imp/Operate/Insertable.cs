using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class Insertable<T, TEntity> : BaseSqlable<T>, IInsertable<T>, ISqlBuilder
    {

        protected readonly TEntity _model;


        public Insertable(IDbConnection connection, TEntity model) : base(connection)
        {
            _model = model;

        }

        public string Build(bool ignorePrefix = true)
        {
            var columnList = new List<string>();
            var parameterList = new List<string>();

            var type = typeof(T);

            var (tableName, properties) = DaoHelper.GetMetas(type);

            foreach (var p in properties)
            {
                if (p.IsIdentity)
                {
                    continue;
                }

                columnList.Add(p.ColumnName);
                parameterList.Add($"{paramterPrefix}{p.Parameter}");

                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(_model));
            }

            StringBuilder sql = new StringBuilder();

            sql.Append($"insert into {tableName} ({string.Join(",", columnList)}) values ({string.Join(",", parameterList)})");

            return sql.ToString();
        }


        public async Task<int> Execute()
        {

            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _model);

                var result = await _connection.ExecuteAsync(sql, _model);

                Aop?.OnExecuted?.Invoke(sql, _model);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _model);
                throw;
            }

        }
    }
}
