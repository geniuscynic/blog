using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   

    public class SelectExpressionVisitor : ExpressionVisitor
    {
        //private readonly Expression _expression;

        public StringBuilder Sql{ get;set;}  = new StringBuilder();


        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.Visit(node.Body);
        }


        protected override Expression VisitNew(NewExpression node)
        {
            foreach (var valueTuple in node.Arguments.Zip(node.Members))
            {
                Sql.Append($"{valueTuple.First} as {valueTuple.Second.Name},");
            }

            Sql.Remove(Sql.Length - 1, 1);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {

            Sql.Append(node);

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
             Sql.Append($"{node}.*");
            return node;
        }
    }
}
