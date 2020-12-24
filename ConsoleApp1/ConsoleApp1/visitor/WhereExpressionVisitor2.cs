using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   

    public class WhereExpressionVisitor2 : ExpressionVisitor
    {
        public int Index { get; private set; }

        public WhereExpressionVisitor2(int index)
        {
            this.Index = index;
        }

        public StringBuilder Sql { get; set; } = new StringBuilder();
        public Dictionary<string, object> dict = new Dictionary<string, object>();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //var whereModel =  new WhereModel();
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
            Sql.Append($"@p{Index}");

            Index++;

            dict.Add($"p{Index}", node.Value);

            //model.value = node.Value;

            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Sql.Append(node);

            //model.Sql.Append(node);


            return base.VisitMember(node);
        }




    }
}
