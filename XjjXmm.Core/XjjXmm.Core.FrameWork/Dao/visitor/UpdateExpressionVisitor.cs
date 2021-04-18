using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp1.Dao.Common;
using DoCare.Extension.Dao.Common;

namespace DoCare.Extension.Dao.visitor
{

    public class UpdateExpressionVisitor : ExpressionVisitor
    {

        public List<Field> UpdatedFields = new List<Field>();
        
        protected override Expression VisitNew(NewExpression node)
        {
           
            foreach (var nodeArgument in node.Arguments)
            {
                var member = ExpressionVistorHelper.VisitMember((nodeArgument as MemberExpression));

                UpdatedFields.Add(member);
                
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            
            
            var member = ExpressionVistorHelper.VisitMember(node);

            UpdatedFields.Add(member);
            

            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
          
            return node;
        }
    }
}
