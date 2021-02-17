using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ConsoleApp1.Dao.Common;

namespace ConsoleApp1.Dao.visitor
{
    

    public class WhereExpressionVisitor : ExpressionVisitor
    {
        public List<WhereModel> Result = new List<WhereModel>();

        private static readonly Dictionary<ExpressionType, string> ExpressionTypeMapping = new Dictionary<ExpressionType, string>()
        {
            {ExpressionType.Equal, " = "},
            {ExpressionType.GreaterThan, " > "},
            {ExpressionType.GreaterThanOrEqual, " >= "},
            {ExpressionType.LessThan, " < "},
            {ExpressionType.LessThanOrEqual, " <= "},
            {ExpressionType.AndAlso, " and "},
        };

      

        public void Run(Expression node)
        {
            //if (Result.Sql.Length > 0)
            //{
            //    Result.Sql.Append(" and ");
            //}
            //else
            //{
            //    Result.Sql.Append(" where ");
            //}

            //Result.Sql.Append(" ( ");
            //Visit(node);
            //Result.Sql.Append(" ) ");
        }

       

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //var whereModel =  new WhereModel();
            

            if (node.Left is BinaryExpression)
            {
                Visit(node.Left);
            }
            else
            {
                var result = Expression.Lambda(node.Left).Compile().DynamicInvoke();

                Result.Add(new WhereModel
                {
                    
                });
            }

            if (ExpressionTypeMapping.ContainsKey(node.NodeType))
            {
                Result.Add(new WhereModel
                {
                    Operator = ExpressionTypeMapping[node.NodeType]
                });
            }

            if (node.Right is  BinaryExpression) 
            {
                
            }
            else
            {
                var result = Expression.Lambda(node.Right).Compile().DynamicInvoke();
            }
            
            Visit(node.Right);

            return node;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            //memberList.Add(node.ToString());
            Result.Sql.Append($"@p{Result.Start}");

            //Result.Parameters.Add($"p{Result.Start}", node.Value);

            //Result.Start++;
            //model.value = node.Value;

            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            
            var member =  ExpressionVistorHelper.VisitMember(node);


            //Result.Prefix = member.Prefix;
            //Result.Sql.Append(member.WhereExpression);
            //Sql.Append(node);

            //model.Sql.Append(node);


            return base.VisitMember(node);
        }

       

    }
}
