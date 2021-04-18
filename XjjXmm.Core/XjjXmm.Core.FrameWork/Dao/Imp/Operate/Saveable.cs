using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DoCare.Extension.Dao.Common;
using DoCare.Extension.Dao.Interface.Command;
using DoCare.Extension.Dao.Interface.Operate;
using DoCare.Extension.Dao.visitor;

namespace DoCare.Extension.Dao.Imp.Operate
{
    public class Saveable<T, TEntity>  : BaseSqlable<T>, ISaveable<T>, ISqlBuilder
    {
       
        protected readonly TEntity _model;

        private readonly UpdateExpressionVisitor _visitor = new UpdateExpressionVisitor();
        private readonly UpdateExpressionVisitor _ignorevisitor = new UpdateExpressionVisitor();

        

        public Saveable(IDbConnection connection, TEntity model): base(connection)
        {
           
            _model = model;

        }

        public ISaveable<T> UpdateColumns<TResult>(Expression<Func<T,TResult>> predicate)
        {
            
            _visitor.Visit(predicate);

            return this;
        }

        public ISaveable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate)
        {
            _ignorevisitor.Visit(predicate);

            return this;
        }


        public string Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, properties) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");

            if (_visitor.UpdatedFields.Any())
            {
                _visitor.UpdatedFields.ForEach(t =>
                {
                    sql.Append($" {t.ColumnName} = {paramterPrefix}{t.Parameter},");
                });

                sql.Remove(sql.Length - 1, 1);
                sql.Append(" where ");

                foreach (var member in properties.Where(t => t.IsPrimaryKey))
                {
                    sql.Append($" {member.ColumnName} = {paramterPrefix}{member.Parameter} and");
                }
            }
            else
            {
                var existProperty = properties.Where(p => !p.IsPrimaryKey && _ignorevisitor.UpdatedFields.All(t => t.ColumnName != p.ColumnName));
                foreach (var p in existProperty)
                {
                    sql.Append($" {p.ColumnName} = {paramterPrefix}{p.Parameter},");
                }

                sql.Remove(sql.Length - 1, 1);
                sql.Append(" where ");

                foreach (var member in properties.Where(t => t.IsPrimaryKey))
                {
                    sql.Append($" {member.ColumnName} = {paramterPrefix}{member.Parameter} and");
                }
            }

            sql.Remove(sql.Length - 3, 3);

            return sql.ToString();
        }

        public async Task<int> Execute()
        {
            var sql = Build();

            try
            {
                Aop?.OnExecuting?.Invoke(sql, _sqlPamater);

                var result = await _connection.ExecuteAsync(sql, _model);

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
