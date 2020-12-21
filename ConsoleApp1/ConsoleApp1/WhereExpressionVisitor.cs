using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WhereExpressionVisitor : ExpressionVisitor
    {


        public StringBuilder Sql {get; set;} = new StringBuilder();

        protected override Expression VisitBinary(BinaryExpression node)
        {

            Visit(node.Left);
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    Sql.Append(" = ");
                    break;

                case ExpressionType.GreaterThan:
                    Sql.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    Sql.Append(" >= ");
                    break;

                case ExpressionType.LessThan:
                    Sql.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    Sql.Append(" <= ");
                    break;

                case ExpressionType.AndAlso:
                    Sql.Append(" and ");
                    break;
            };
            Visit(node.Right);
            return node;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            //memberList.Add(node.ToString());
            Sql.Append(node.ToString());
            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {

            Sql.Append(node);

            return base.VisitMember(node);
        }




    }
}
