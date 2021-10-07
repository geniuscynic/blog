using System;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Zkzx.Core.Database.Imp.Command;
using DoCare.Zkzx.Core.Database.Interface.Command;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;


namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    internal abstract class Updateable<T> : BaseOperate, IUpdateable<T>
    {
        
        private readonly IWhereCommand whereCommand;

        private readonly StringBuilder setSql = new StringBuilder();


        public Updateable(DbInfo info) : base(info)
        {
            whereCommand = new WhereCommand(_providerModel, CreateWhereProvider(_providerModel));
        }

        public IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate)
        {
            var setProvider = new SetProvider();
            setProvider.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();


            setProvider.UpdatedFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                setSql.Append($" {t.ColumnName} = {_providerModel.DbInfo.StatementPrefix}{t.Parameter},");

                _providerModel.Parameter[t.Parameter] = values;
            });

            return this;
        }

        public IUpdateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;
        }

        public IUpdateable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);
            return this;
        }

        public IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }


        private StringBuilder Build()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = ProviderHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");


            sql.Append(setSql);

            sql.Remove(sql.Length - 1, 1);

            sql.Append(whereCommand.Build());

            
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
