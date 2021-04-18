﻿using System.Collections.Generic;
using System.Linq.Expressions;
using DoCare.Extension.DataBase.Utility;


namespace DoCare.Extension.DataBase.SqlProvider
{

    public class SetProvider : ExpressionVisitor
    {

        public List<Field> UpdatedFields = new List<Field>();


     
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            var field = ProviderHelper.VisitMember(node.Member);

            UpdatedFields.Add(field);

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Members == null) return base.VisitNew(node);

            foreach (var nodeMember in node.Members)
            {
                var field = ProviderHelper.VisitMember(nodeMember);

                UpdatedFields.Add(field);
            }

            return node;
        }
    }
}
