using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ConsoleApp1.Dao.Interface.Command;
using ConsoleApp1.Dao.visitor;

namespace ConsoleApp1.Dao.Imp.Command
{
    internal class WhereCommand<T> : IWhereCommand<T>
    {
        private readonly Dictionary<string, object> _sqlPamater;
        private readonly WhereExpressionVisitor _wherevisitor = new WhereExpressionVisitor();

        private readonly StringBuilder _whereCause = new StringBuilder();

        private string prefix = "";

        public WhereCommand(Dictionary<string, object> sqlPamater)
        {
            _sqlPamater = sqlPamater;
        }

        public void Where(Expression<Func<T, bool>> predicate)
        {
            _wherevisitor.Visit(predicate);

            _wherevisitor.whereModel.Sql.Append(" and");

            prefix = _wherevisitor.whereModel.Prefix;
        }

        public void Where(string whereExpression)
        {
            _whereCause.Append($" ({whereExpression}) and");
        }

        public void Where<TResult>(string whereExpression, Expression<Func<TResult>> predicate)
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

                _sqlPamater[t.Parameter] = values;
            });
        }

        public string Build(bool ignorePrefix = true)
        {
            var sql = new StringBuilder();
            sql.Append(" where ");

            sql.Append(_whereCause);

            sql.Append(_wherevisitor.whereModel.Sql);
            sql.Remove(sql.Length - 3, 3);

            foreach (var keyValuePair in _wherevisitor.whereModel.Parameter)
            {
                _sqlPamater[keyValuePair.Key] = keyValuePair.Value;
            }



            return sql.ToString();
        }
    }
}
