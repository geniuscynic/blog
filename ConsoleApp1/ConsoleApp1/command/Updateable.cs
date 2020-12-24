using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Updateable<T>
    {
        private readonly SqlConnection _connection;
        private readonly T _model;

        UpdateExpressionVisitor _visitor = new UpdateExpressionVisitor();
        UpdateExpressionVisitor _ignorevisitor = new UpdateExpressionVisitor();
        private readonly List<Expression> _whereExpressionList = new List<Expression>();
        private int index = 0;
        Dictionary<string, object> dict = new Dictionary<string, object>();

        public Updateable(SqlConnection connection, T model)
        {
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

            _ignorevisitor.Visit(predicate);

            return this;
        }

        public Updateable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _whereExpressionList.Add(predicate);
            return this;
        }

        private string BuildWhere()
        {
            var sqls = new List<string>();
                        
            _whereExpressionList.ForEach(t =>
            {
                var vistor = new WhereExpressionVisitor2(index);
                vistor.Visit(t);
                index = vistor.Index + 1;

                sqls.Add($"({vistor.Sql})");

                foreach (var keyValuePair in vistor.dict)
                {
                    dict.Add(keyValuePair.Key, keyValuePair.Value);
                }
            });


            _whereExpressionList.Clear();

            return sqls.Count switch
            {
                0 => string.Join(" and ", sqls),
                _ => " where " + string.Join(" and ", sqls)
            };
        }

        private StringBuilder buildSql()
        {
            var sql = new StringBuilder();

            sql.Append("update t set ");

            dynamic model = new object();

            dynamic dobj = new System.Dynamic.ExpandoObject();

            var dic = (IDictionary<string, object>)dobj;
          

            _visitor.UpdateModels.ForEach(t =>
            {
                var property = _model.GetType().GetProperties().Single(x => x.Name == t.oriFieldName);


                sql.Append($"{t.fieldName}=@{t.paramterName},");

                dic[t.oriFieldName] = property.GetValue(_model);

            });


            if (_ignorevisitor.UpdateModels.Count > 0)
            {
                foreach (var propertyInfo in _model.GetType().GetProperties())
                {
                    if (!_ignorevisitor.UpdateModels.Any(t => t.oriFieldName == propertyInfo.Name))
                    {
                        sql.Append($"{propertyInfo.Name}=@{propertyInfo.Name},");

                        dic[propertyInfo.Name] = propertyInfo.GetValue(_model);
                    }
                }
            }
            

            sql.Remove(sql.Length - 1, 1);

            sql.Append(" from ");
            sql.Append(_model.GetType().Name);
            sql.Append($" {_visitor.UpdateModels.First().Prefix}");


            var where = BuildWhere();

            if (string.IsNullOrEmpty(where))
            {
                foreach (var p in _model.GetType().GetProperties())
                {
                    if (p.Name.ToLowerInvariant() == "id")
                    {
                        sql.Append($" where id = @{p.Name}");
                        break;
                    }
                }
            }
            else
            {
                foreach (var keyValuePair in dict)
                {
                    dic[keyValuePair.Key] = keyValuePair.Value;
                    
                }

                sql.Append(" " + where);
            }
            

            




            //StringBuilder sql = new StringBuilder();

            //sql.Append($"insert into {_model.GetType().Name} ({string.Join(",", p1)}) values ({string.Join(",", p2)});");

            return sql;
        }
        public int Execute()
        {
            
            var sql = buildSql();

            var result = 1; //_connection.ExecuteScalar<int>(sql.ToString(), _model);

            return result;
        }


    }
}
