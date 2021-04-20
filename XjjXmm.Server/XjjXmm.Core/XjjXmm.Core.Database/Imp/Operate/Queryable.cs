using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.Core.Database.Imp.Command;
using XjjXmm.Core.Database.Imp.Command.MsSql;
using XjjXmm.Core.Database.Interface.Command;
using XjjXmm.Core.Database.Interface.Operate;
using XjjXmm.Core.Database.SqlProvider;
using XjjXmm.Core.Database.Utility;

namespace XjjXmm.Core.Database.Imp.Operate
{
    public abstract class Queryable<T> : BaseOperate, IDoCareQueryable<T>
    {
        private readonly IWhereCommand whereCommand;
        private readonly IOrderByCommand<T> orderByCommand;

        private readonly StringBuilder _selectField = new StringBuilder();

        
        private string prefix = "";

        //private readonly  StringBuilder _sortSql = new StringBuilder();
        
        public Queryable(IDbConnection connection)  : base(connection)
        {
            whereCommand = new WhereCommand(_providerModel);

            orderByCommand = new OrderByCommand<T>();
        }

        public IDoCareQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;

        }

        public IDoCareQueryable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);

            return this;
        }

        public IDoCareQueryable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }

        public IDoCareQueryable<T> OrderBy<TResult>(Expression<Func<T, TResult>> predicate)
        {
            orderByCommand.AscBy(predicate);


            return this;
        }

        public IDoCareQueryable<T> OrderByDesc<TResult>(Expression<Func<T, TResult>> predicate)
        {
            orderByCommand.DescBy(predicate);

            return this;
        }

        protected abstract IReaderableCommand<TResult> CreateReaderableCommand<TResult>(IDbConnection connection, StringBuilder sql,
            Dictionary<string, object> sqlParameter, Aop aop);
            //{
             //return new ReaderableCommand<T>(connection, Build(), _providerModel.Parameter, aop);
        //}

        public IReaderableCommand<TResult> Select<TResult>(Expression<Func<T,TResult>> predicate)
        {
            var provider = new SelectProvider();
            provider.Visit(predicate);

            provider.SelectFields.ForEach(t =>
            {
                _selectField.Append($"{t.Prefix}.{t.ColumnName} as {t.Parameter},");

                prefix = t.Prefix;
            });
            _selectField.Remove(_selectField.Length - 1, 1);
            return CreateReaderableCommand<TResult>(Connection, Build(), _providerModel.Parameter, Aop);
            // return DatabaseFactory.CreateReaderableCommand<TResult>(Connection, Build(), _providerModel.Parameter, Aop);
        }


        private StringBuilder Build()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = ProviderHelper.GetMetas(type);

            var selectSql = new StringBuilder();
            if (_selectField.Length > 0)
            {
                selectSql.Append( _selectField);
            }
            else
            {
                foreach (var property in properties)
                {
                    selectSql.Append($"{property.ColumnName} as {property.Parameter},");
                }

                selectSql.Remove(selectSql.Length - 1, 1);
            }


            sql.Append($"select {selectSql} from {tableName} ");

            
            sql.Append(whereCommand.Build());

            sql.Append(orderByCommand.Build());

            return sql;
        }


        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteQuery();
        }

        public async Task<T> ExecuteFirst()
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteFirst();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteSingle()
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteSingle();
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteSingleOrDefault();
        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            var command = CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ToPageList(pageIndex, pageSize);

        }
    }
}
