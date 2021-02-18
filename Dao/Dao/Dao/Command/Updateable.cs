﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Dao.Common;
using ConsoleApp1.Dao.Operate;
using ConsoleApp1.Dao.visitor;
using Dapper;

namespace ConsoleApp1.Dao.Command
{
    public class Updateable<T> : IUpdateable<T>
    {
        private readonly IDbConnection _connection;



        private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        private readonly StringBuilder _whereCause = new StringBuilder();

        private readonly StringBuilder setSql = new StringBuilder();

     

        private Dictionary<string, object> _dynamicModel = new Dictionary<string, object>();

        public Updateable(IDbConnection connection)
        {
            _connection = connection;

        }

        public IUpdateable<T> SetColumns<TResult>(Expression<Func<TResult>> predicate)
        {
            var visitor = new NewObjectExpressionVisitor();
            visitor.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();


            visitor.UpdatedFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                setSql.Append($" {t.ColumnName} = @{t.Parameter},");

                _dynamicModel[t.Parameter] = values;
            });

            return this;
        }

        public IUpdateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _wherevisitor.Visit(predicate);

            return this;
        }

        public IUpdateable<T> Where(string whereExpression)
        {
            _whereCause.Append($" ({whereExpression}) and");
            return this;
        }

        public IUpdateable<T> Where<TEntity>(string whereExpression, Expression<Func<TEntity>> predicate)
        {
            _whereCause.Append($" ({whereExpression}) and");

            var visitor = new NewObjectExpressionVisitor();
            visitor.Visit(predicate);

            //var dic = (IDictionary<string, object>)_dynamicModel;

            var model = predicate.Compile().Invoke();
            var types = model.GetType();


            visitor.UpdatedFields.ForEach(t =>
            {
                var values = types.GetProperty(t.Parameter)?.GetValue(model);

                _dynamicModel[t.Parameter] = values;
            });

            return this;
          
        }


        private StringBuilder BuildSql()
        {
            var sql = new StringBuilder();

            var type = typeof(T);
            var (tableName, _) = DaoHelper.GetMetas(type);

            sql.Append($"update {tableName} set ");


            sql.Append(setSql);

            sql.Remove(sql.Length - 1, 1);

            sql.Append(" where ");

            sql.Append(_whereCause);

            sql.Remove(sql.Length - 3, 3);


            return sql;
        }

        public async Task<int> Execute()
        {

            var sql = BuildSql();


            var result = await _connection.ExecuteAsync(sql.ToString(), _dynamicModel);

            return 1;
        }


    }
}