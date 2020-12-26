using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SelectModel
    {
        public StringBuilder Sql { get; set; } = new StringBuilder();

        public string TableName { get; set; } 
    }
    public class SelectExpressionVisitor : ExpressionVisitor
    {
        //private readonly Expression _expression;

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
                Result.Sql.Append($"{valueTuple.First} as {valueTuple.Second.Name},");
            }

            Result.Sql.Remove(Result.Sql.Length - 1, 1);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {

            Result.Sql.Append(node);

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
             Result.Sql.Append($"{node}.*");
            return node;
        }
    }
}
