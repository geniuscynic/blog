using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using XjjXmm.DataBase.Imp.Command;
using XjjXmm.DataBase.Imp.Command.MsSql;
using XjjXmm.DataBase.Interface.Command;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Operate
{
    [Obsolete(message: "推荐使用：ComplexQueryable")]
    internal abstract class Queryable<T> : BaseOperate, IDoCareQueryable<T>
    {
        private readonly IWhereCommand whereCommand;
        private readonly IOrderByCommand<T> orderByCommand;

        private readonly StringBuilder _selectField = new StringBuilder();

        
        private string prefix = "";

        //private readonly  StringBuilder _sortSql = new StringBuilder();

        public Queryable(DbInfo dbInfo) : base(dbInfo)
        {
            whereCommand = new WhereCommand(_providerModel, CreateWhereProvider(_providerModel));

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

        protected abstract IReaderableCommand<TResult> CreateReaderableCommand<TResult>(DbInfo dbInfo, StringBuilder sql,
            Dictionary<string, object> sqlParameter);
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
            return CreateReaderableCommand<TResult>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
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
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteQuery();
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2>(Func<T, T2, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2, T3>(Func<T, T2, T3, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4>(Func<T, T2, T3, T4, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5>(Func<T, T2, T3, T4, T5, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6>(Func<T, T2, T3, T4, T5, T6, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T2, T3, T4, T5, T6, T7>(Func<T, T2, T3, T4, T5, T6, T7, T> func, params string[] splitOn)
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            return await command.ExecuteQuery(func, splitOn);
        }

        public async Task<T> ExecuteFirst()
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteFirst();
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteFirstOrDefault();
        }

        public async Task<T> ExecuteSingle()
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteSingle();
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ExecuteSingleOrDefault();
        }

        public async Task<(IEnumerable<T> data, int total)> ToPageList(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new Exception("pageIndex 不能小于1页");
            }

            if (pageSize < 1)
            {
                throw new Exception("pageSize 不能小于1条");
            }

            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);
            //DatabaseFactory.CreateReaderableCommand<T>(Connection, Build(), _providerModel.Parameter, Aop);

            return await command.ToPageList(pageIndex, pageSize);

        }

        public async Task<DataTable> ExecuteDataTable()
        {
            var command = CreateReaderableCommand<T>(_providerModel.DbInfo, Build(), _providerModel.Parameter);

            return await command.ExecuteDataTable();
        }

        protected abstract WhereProvider CreateWhereProvider(ProviderModel providerModel);
    }
}
