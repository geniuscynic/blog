using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.attribute;
using ConsoleApp1.visitor3;

namespace ConsoleApp1
{
    public class SelectModel
    {
        public StringBuilder Sql { get; set; } = new StringBuilder();

        public string TableName { get; set; }
    }
    public class SelectExpressionVisitor : ExpressionVisitor
    {
        //private readonly WhereExpression _expression;

        public SelectModel Result { get; } = new SelectModel();


        public void Run(Expression node)
        {
            Result.Sql.Append("select ");
            var expression = node as LambdaExpression;

            Result.TableName = expression.Parameters.First().Name;

            base.Visit(expression.Body);

            Result.Sql.Append(" from ");
        }
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.Visit(node.Body);
        }


        protected override Expression VisitNew(NewExpression node)
        {
            foreach (var valueTuple in node.Arguments.Zip(node.Members))
            {
                var member = XjjxmmExpressionVistorHelper.VisitMember(valueTuple.First as MemberExpression);

                Result.Sql.Append($"{member.WhereExpression} as {valueTuple.Second.Name},");
            }

            Result.Sql.Remove(Result.Sql.Length - 1, 1);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var member = XjjxmmExpressionVistorHelper.VisitMember(node);


            Result.Sql.Append(member.WhereExpression);


            //Result.Sql.Append(node);

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            
            foreach (var field in XjjxmmExpressionVistorHelper.VisitProperty(node.Type.GetProperties(), node.Name))
            {
                Result.Sql.Append($"{field.SelectExpression},");
            }

            Result.Sql.Remove(Result.Sql.Length - 1, 1);

            return node;
        }
    }
}
