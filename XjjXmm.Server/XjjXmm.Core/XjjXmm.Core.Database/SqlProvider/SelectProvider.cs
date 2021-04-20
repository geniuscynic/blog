using System.Collections.Generic;
using System.Linq.Expressions;
using XjjXmm.Core.Database.Utility;

namespace XjjXmm.Core.Database.SqlProvider
{

    public class SelectProvider : ExpressionVisitor
    {

        public List<Field> SelectFields { get; set; } = new List<Field>();

        protected override Expression VisitNew(NewExpression node)
        {
           
            foreach (var nodeArgument in node.Arguments)
            {
                var member = ProviderHelper.VisitMember((nodeArgument as MemberExpression));

                SelectFields.Add(member);
                
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            
            
            var member = ProviderHelper.VisitMember(node);

            SelectFields.Add(member);
            

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
          
            return node;
        }
    }
}
