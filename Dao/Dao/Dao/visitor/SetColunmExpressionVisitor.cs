using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp1.Dao.Common;

namespace ConsoleApp1.Dao.visitor
{

    public class SetColunmExpressionVisitor : ExpressionVisitor
    {

        public List<Field> UpdatedFields = new List<Field>();


     
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            var field = ExpressionVistorHelper.VisitMember(node.Member);

            UpdatedFields.Add(field);

            return node;
        }

       

    }
}
