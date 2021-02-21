using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Interface.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Command
{
    public class Saveable<T, TEntity>  : ISaveable<T>
    {
        private readonly IDbConnection _connection;
        private readonly TEntity _model;

        private readonly UpdateExpressionVisitor _visitor = new UpdateExpressionVisitor();
        private readonly UpdateExpressionVisitor _ignorevisitor = new UpdateExpressionVisitor();

        public Saveable(IDbConnection connection, TEntity model)
        {
            _connection = connection;
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

        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();
            
            var type = typeof(T);
            var (tableName, properties) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");

            if (_visitor.UpdatedFields.Any())
            {
                _visitor.UpdatedFields.ForEach(t =>
                {
                    sql.Append($" {t.ColumnName} = @{t.Parameter},");
                });

                sql.Remove(sql.Length - 1, 1);
                sql.Append(" where ");

                foreach (var member in properties.Where(t=>t.IsPrimaryKey))
                {
                    sql.Append($" {member.ColumnName} = @{member.Parameter} and");
                }
            }
            else
            {
                var existProperty = properties.Where(p => !p.IsPrimaryKey && _ignorevisitor.UpdatedFields.All(t => t.ColumnName != p.ColumnName));
                foreach (var p in existProperty)
                {
                    sql.Append($" {p.ColumnName} = @{p.Parameter},");
                }

                sql.Remove(sql.Length - 1, 1);
                sql.Append(" where ");

                foreach (var member in properties.Where(t=>t.IsPrimaryKey))
                {
                    sql.Append($" {member.ColumnName} = @{member.Parameter} and");
                }
            }

            sql.Remove(sql.Length - 3, 3);
           
            return sql;
        }
        
        public async Task<int> Execute()
        {

            var sql = BuildSql();


           var result = await _connection.ExecuteAsync(sql.ToString(), _model);

            return result;
        }


    }
}
