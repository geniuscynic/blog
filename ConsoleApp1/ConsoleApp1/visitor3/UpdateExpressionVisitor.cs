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
    //public class UpdateModel
    //{
    //    public string oriFieldName { get; set; }

    //    public string fieldName { get; set; }
    //    public string paramterName { get; set; }


    //    public string Prefix { get; set; }
    //}
    public class UpdateExpressionVisitor : ExpressionVisitor
    {

        public List<Member> UpdateModels = new List<Member>();
        //private readonly WhereExpression _expression;

        //public StringBuilder Sql { get; set; } = new StringBuilder();


        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.Visit(node.Body);
        }


        protected override Expression VisitNew(NewExpression node)
        {
            //foreach (var valueTuple in node.Arguments.Zip(node.Members))
            //{
            //    Sql.Append($"{valueTuple.First} as {valueTuple.Second.Name},");
            //}

            //Sql.Remove(Sql.Length - 1, 1);
            foreach (var nodeArgument in node.Arguments)
            {
                var member = XjjxmmExpressionVistorHelper.VisitMember(nodeArgument as MemberExpression);

                UpdateModels.Add(member);
                //Visit(nodeArgument);
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var expression = node.Expression as ParameterExpression;
            

            //UpdateModels.Add(new UpdateModel
            //{
            //    fieldName = node.Member.Name.ToLowerInvariant(),
            //    paramterName = node.Member.Name,
            //    oriFieldName = node.Member.Name,
            //    Prefix = expression.Name
            //});


            var member = XjjxmmExpressionVistorHelper.VisitMember(node);

            UpdateModels.Add(member);
            // Sql.Append($"{node.Member.Name.ToLowerInvariant()} = @{node.Member.Name.ToLowerInvariant()}");

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            //Sql.Append($"{node}.*");
            return node;
        }
    }
}
