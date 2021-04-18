using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ConsoleApp1.Dao.Common;
using DoCare.Extension.Dao.Common;

namespace DoCare.Extension.Dao.visitor
{


    public class WhereExpressionVisitor : ExpressionVisitor
    {
        //public List<WhereModel> Result = new List<WhereModel>();

        public readonly WhereModel whereModel = new WhereModel();

        private static readonly Dictionary<ExpressionType, string> ExpressionTypeMapping = new Dictionary<ExpressionType, string>()
        {
            {ExpressionType.Equal, " = "},
            {ExpressionType.GreaterThan, " > "},
            {ExpressionType.GreaterThanOrEqual, " >= "},
            {ExpressionType.LessThan, " < "},
            {ExpressionType.LessThanOrEqual, " <= "},
            {ExpressionType.AndAlso, " and "},
            {ExpressionType.OrElse, " or "},
        };

        private int start = 0;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            whereModel.Sql.Append("(");

            Visit(node.Left);
            


            if (ExpressionTypeMapping.ContainsKey(node.NodeType))
            {
                whereModel.Sql.Append(ExpressionTypeMapping[node.NodeType]);
            }


            Visit(node.Right);

            whereModel.Sql.Append(")");

            return node;
        }

        private void AddConstant(object result)
        {
            whereModel.Sql.Append($"{DatabaseFactory.ParamterSplit}p{start}");

            whereModel.Parameter[$"p{start}"] = result;

            start++;

        }
        protected override Expression VisitConstant(ConstantExpression node)
        {


            //Expression<Func<object>> expression = () => node.Value;
            AddConstant(node.Value);


            //memberList.Add(node.ToString());
            //Result.Sql.Append($"@p{Result.Start}");

            //Result.Parameters.Add($"p{Result.Start}", node.Value);

            //Result.Start++;
            //model.value = node.Value;

            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression)
            {

                var member = ExpressionVistorHelper.VisitMember(node);

                whereModel.Prefix = member.Prefix;
                whereModel.Sql.Append(member.Express);
            }
            else if (node.Expression is ConstantExpression)
            {
                var expression = node.Expression;

                object container = ((ConstantExpression)expression).Value;
                var member = node.Member;
                if (member is FieldInfo)
                {
                    object value = ((FieldInfo)member).GetValue(container);

                    AddConstant(value);
                    return Expression.Constant(value);
                }

                if (member is PropertyInfo)
                {
                    object value = ((PropertyInfo)member).GetValue(container, null);

                    AddConstant(value);
                    return Expression.Constant(value);
                }

            }
            else if(node.Expression is MemberExpression && node.Member is PropertyInfo)
            {
                var expression = (MemberExpression) node.Expression;
                object container = ((ConstantExpression)expression.Expression).Value;
                var obj1 = ((FieldInfo)expression.Member).GetValue(container);

                object value = ((PropertyInfo)node.Member).GetValue(obj1, null);

                AddConstant(value);
                return Expression.Constant(value);
            }
           

            //Result.Prefix = member.Prefix;
            //Result.Sql.Append(member.WhereExpression);
            //Sql.Append(node);

            //model.Sql.Append(node);


            return base.VisitMember(node);
        }


     

    }
}
