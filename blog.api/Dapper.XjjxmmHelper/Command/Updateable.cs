using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper.XjjxmmHelper.Visitor;

namespace Dapper.XjjxmmHelper.Command
{
    /// <summary>
    /// 需要where
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Updateable<T>
    {
        private readonly IDbConnection _connection;
        private readonly T _model;

        private UpdateExpressionVisitor _visitor = new UpdateExpressionVisitor();
        private readonly UpdateExpressionVisitor _ignoreVisitor = new UpdateExpressionVisitor();

        private readonly WhereExpressionVisitor _whereVisitor;

        //private int index = 0;
        Dictionary<string, object> dict = new Dictionary<string, object>();

        public Updateable(IDbConnection connection, T model)
        {
            _whereVisitor = new WhereExpressionVisitor();

            _connection = connection;
            _model = model;
        }


        public Updateable<T> UpdateColumns<TResult>(Expression<Func<T, TResult>> predicate)
        {

            _visitor.Visit(predicate);

            return this;
        }

        public Updateable<T> IgnoreColumns<TResult>(Expression<Func<T, TResult>> predicate)
        {

            _ignoreVisitor.Visit(predicate);

            return this;
        }

        public Updateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _whereVisitor.Run(predicate);

            return this;
        }

        //private string BuildWhere()
        //{
        //    var sqls = new List<string>();

        //    _whereExpressionList.ForEach(t =>
        //    {
        //        var vistor = new WhereExpressionVisitor(index);
        //        vistor.Visit(t);
        //        index = vistor.Index + 1;

        //        sqls.Add($"({vistor.Sql})");

        //        foreach (var keyValuePair in vistor.dict)
        //        {
        //            dict.Add(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    });


        //    _whereExpressionList.Clear();

        //    return sqls.Count switch
        //    {
        //        0 => string.Join(" and ", sqls),
        //        _ => " where " + string.Join(" and ", sqls)
        //    };
        //}

        private StringBuilder buildSql()
        {
            var sql = new StringBuilder();

            var prefix = "";
            if (_whereVisitor.Result.Sql.Length == 0)
            {
                sql.Append($"update {typeof(T).Name} set ");
            }
            else
            {
                prefix = _whereVisitor.Result.Prefix + ".";
                sql.Append($"update {_whereVisitor.Result.Prefix} set ");
            }
            //dynamic model = new object();

            dynamic dobj = new System.Dynamic.ExpandoObject();

            var dic = (IDictionary<string, object>)dobj;


            if (_visitor.UpdateModels.Count > 0)
            {
                _visitor.UpdateModels.ForEach(t =>
                {
                    var property = _model.GetType().GetProperties().Single(x => x.Name == t.OriginFieldName);


                    sql.Append($"{prefix}{t.FieldName}=@{t.OriginFieldName},");

                    dic[t.OriginFieldName] = property.GetValue(_model);

                });
            }

            else
            {
                var properties = XjjxmmExpressionVistorHelper.VisitProperty(_model.GetType().GetProperties(), "");
                foreach (var propertyInfo in properties)
                {
                    if (_whereVisitor.Result.Sql.Length == 0 && propertyInfo.IsKey)
                    {
                        _whereVisitor.Result.Sql.Append(
                            $"where {prefix}{propertyInfo.FieldName} = @{propertyInfo.OriginFieldName}");  

                        dic[propertyInfo.OriginFieldName] = propertyInfo.PropertyInfo.GetValue(_model);
                    }

                    if (_ignoreVisitor.UpdateModels.Count > 0)
                    {
                        if (_ignoreVisitor.UpdateModels.Any(t => t.OriginFieldName == propertyInfo.OriginFieldName)) continue;
                    }



                    sql.Append($"{prefix}{propertyInfo.FieldName}=@{propertyInfo.OriginFieldName},");

                    dic[propertyInfo.OriginFieldName] = propertyInfo.PropertyInfo.GetValue(_model);

                }
            }


            sql.Remove(sql.Length - 1, 1);

            sql.Append(" from ");
            sql.Append(_model.GetType().Name);

            if (prefix.Length > 0)
            {
                prefix = prefix.Substring(0, prefix.Length - 2);
                sql.Append($" {prefix} ");
            }

            sql.Append(_whereVisitor.Result.Sql);


            return sql;
        }
        public int Execute()
        {

            var sql = buildSql();

            var result = _connection.Execute(sql.ToString(), _model);

            return result;
        }


        public async Task<int> ExecuteAsync()
        {

            var sql = buildSql();

            var result = await _connection.ExecuteAsync(sql.ToString(), _model);

            return result;
        }



    }
}
