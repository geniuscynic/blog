using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //public class WhereModel
    //{
    //    //public string Left { get; set; }
    //    //public string Operator { get; set; }
    //    //public string right { get; set; }

    //    public string Paramter { get; set; } = "@paramter";

    //    public object ParamterValue { get; set; } = "";

    //    public StringBuilder Sql { get; set; } = new StringBuilder();
    //}

    public class WhereExpressionVisitor3 : ExpressionVisitor
    {
              public List<ExpandoObject> WhereModels = new List<ExpandoObject>();

              dynamic model = new ExpandoObject();
        public StringBuilder Sql {get; set;} = new StringBuilder();

        protected override Expression VisitBinary(BinaryExpression node)
        {
             model = new ExpandoObject();
            WhereModels.Add(model);

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
            Sql.Append("{0}");

            model.value = node.Value;

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
