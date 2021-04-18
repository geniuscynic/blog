using System.Collections.Generic;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Utility;


namespace DoCare.Extension.DataBase.SqlProvider
{

    public class WhereProvider : ExpressionVisitor
    {
        private readonly ProviderModel _providerModel;
        //public List<WhereModel> Result = new List<WhereModel>();

        public readonly WhereModel whereModel;

        public WhereProvider(ProviderModel providerModel)
        {
            _providerModel = providerModel;
            whereModel = new WhereModel();
        }

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

        protected override Expression VisitBinary(BinaryExpression node)
        {
            whereModel.Sql.Append(" (");

            Visit(node.Left);
            


            if (ExpressionTypeMapping.ContainsKey(node.NodeType))
            {
                whereModel.Sql.Append(ExpressionTypeMapping[node.NodeType]);
            }

            //var result  = Expression.Lambda(node.Right).Compile().DynamicInvoke();
            Visit(node.Right);

            whereModel.Sql.Append(") ");

            return node;
        }

        private void AddConstant(object result)
        {
            whereModel.Sql.Append($"{_providerModel.DataParamterPrefix}p{_providerModel.Start}");

            _providerModel.Parameter[$"p{_providerModel.Start}"] = result;

            _providerModel.Start++;

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

                var member = ProviderHelper.VisitMember(node);

                whereModel.Prefix = member.Prefix;
                whereModel.Sql.Append(member.Express);
            }
            else 
            {
              

                var value = Expression.Lambda(node).Compile().DynamicInvoke();
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
