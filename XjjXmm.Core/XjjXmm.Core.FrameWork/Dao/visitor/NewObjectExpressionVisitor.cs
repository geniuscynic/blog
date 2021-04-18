using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp1.Dao.Common;
using DoCare.Extension.Dao.Common;

namespace DoCare.Extension.Dao.visitor
{

    public class NewObjectExpressionVisitor : ExpressionVisitor
    {

        public List<Field> UpdatedFields = new List<Field>();


     
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            var field = ExpressionVistorHelper.VisitMember(node.Member);

            UpdatedFields.Add(field);

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Members == null) return base.VisitNew(node);

            foreach (var nodeMember in node.Members)
            {
                var field = ExpressionVistorHelper.VisitMember(nodeMember);

                UpdatedFields.Add(field);
            }

            return node;
        }
    }
}
