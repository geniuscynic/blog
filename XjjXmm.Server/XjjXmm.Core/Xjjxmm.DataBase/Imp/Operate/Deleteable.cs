using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate
{
    internal abstract class Deleteable<T> : BaseOperate, IDeleteable<T>
    {
        private readonly  IWhereCommand whereCommand;

        public Deleteable(DbInfo dbInfo) : base(dbInfo)
        {
            whereCommand = new WhereCommand(_providerModel, CreateWhereProvider(_providerModel));
        }

        public IDeleteable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;
        }

        public IDeleteable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);

            return this;
        }

        public IDeleteable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }

        private StringBuilder Build()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = ProviderHelper.GetMetas(type);

            sql.Append($"delete from {tableName}  ");

            //sql.Append(" where ");
            var where = whereCommand.Build();

            if (where.Length > 0)
            {
                sql.Append(where);
            }
            else
            {
                sql.Append(" where ");

                foreach (var member in properties.Where(t => t.IsPrimaryKey))
                {
                    sql.Append($" {member.ColumnName} = {_providerModel.DbInfo.StatementPrefix}{member.Parameter} and");
                }

                sql.Remove(sql.Length - 3, 3);
            }



            //sql.Remove(sql.Length - 3, 3);


            return sql;
        }

        public async Task<int> Execute()
        {
            //var command = new WriteableCommand(_providerModel.DbInfo, Build().ToString(), _providerModel.Parameter);
            var command = CreateWriteableCommand(_providerModel.DbInfo, Build().ToString(), _providerModel.Parameter);
            return await command.Execute();

        }

        protected abstract WhereProvider CreateWhereProvider(ProviderModel providerModel);
        protected abstract IWriteableCommand CreateWriteableCommand(DbInfo dbInfo, string sql, object sqlParameter);
    }
}
