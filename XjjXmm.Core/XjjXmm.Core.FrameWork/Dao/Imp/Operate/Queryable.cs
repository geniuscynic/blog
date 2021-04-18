using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Imp.Command;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;
using DoCare.Extension.Dao.visitor;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class Queryable<T> : BaseSqlable<T>, IXXQueryable<T>, ISqlBuilder
    {
       

        private readonly IWhereCommand<T> whereCommand;

       

        private readonly StringBuilder _selectField = new StringBuilder();

        

        private string prefix = "";

        public Queryable(IDbConnection connection)  : base(connection)
        {
          

            whereCommand = new WhereCommand<T>(_sqlPamater);

          

        }

        public IXXQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            whereCommand.Where(predicate);

            return this;

        }

        public IXXQueryable<T> Where(string whereExpression)
        {
            whereCommand.Where(whereExpression);

            return this;
        }

        public IXXQueryable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            whereCommand.Where(whereExpression, predicate);

            return this;
          
        }

        public IQueryCommand<TResult> Select<TResult>(Expression<Func<T,TResult>> predicate)
        {
            

            var visitor = new UpdateExpressionVisitor();
            visitor.Visit(predicate);

            visitor.UpdatedFields.ForEach(t =>
            {
                _selectField.Append($"{t.Prefix}.{t.ColumnName} as {t.Parameter},");

                prefix = t.Prefix;
            });
            _selectField.Remove(_selectField.Length - 1, 1);
            return new SimpleQueryable<TResult>(_connection, Build(), _sqlPamater)
            {
                Aop = Aop
            };
        }


        public string Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = DaoHelper.GetMetas(type);

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


            sql.Append($"select {selectSql} from {tableName} {prefix} ");



            sql.Append(whereCommand.Build().Replace(DatabaseFactory.ParamterSplit, paramterPrefix));

            return sql.ToString();
        }


        public async Task<IEnumerable<T>> ExecuteQuery()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteFirst()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryFirstAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteFirstOrDefault()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QueryFirstOrDefaultAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteSingle()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QuerySingleAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }

        public async Task<T> ExecuteSingleOrDefault()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.QuerySingleOrDefaultAsync<T>(sql, _sqlPamater);

                Aop?.OnExecuted?.Invoke(sql, _sqlPamater);

                return result;
            }
            catch
            {
                Aop?.OnError?.Invoke(sql, _sqlPamater);
                throw;
            }
        }
    }
}
