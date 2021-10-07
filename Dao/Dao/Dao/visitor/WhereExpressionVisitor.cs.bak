using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ConsoleApp1.Dao.Common;

namespace ConsoleApp1.Dao.visitor
{
    

    public class WhereExpressionVisitor : ExpressionVisitor
    {
        //public List<WhereModel> Result = new List<WhereModel>();

        public readonly WhereModel whereModel = new WhereModel();

        private static readonly Dictionary<ExpressionType, string> ExpressionTypeMapping = new Dictionary<ExpressionType, string>()
        {
            {ExpressionType.Equal, " = "},
            {ExpressionType.GreaterThan, " > "},
            {ExpressionType.GreaterThanOrEqual, " >= "},
            {ExpressionType.LessThan, " < "},
            {ExpressionType.LessThanOrEqual, " <= "},
            {ExpressionType.AndAlso, " and "},
        };

        private int start = 0;

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
            
            Visit(node.Left);
            

            if (ExpressionTypeMapping.ContainsKey(node.NodeType))
            {
                whereModel.Sql.Append(ExpressionTypeMapping[node.NodeType]);
            }

            
            Visit(node.Right);

            return node;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            whereModel.Sql.Append($"@p{start}");

            //Expression<Func<object>> expression = () => node.Value;
            var result = Expression.Lambda(node).Compile().DynamicInvoke();

           

            whereModel.Parameter[$"p{start}"] = result;

            start++;
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

                var member = ExpressionVistorHelper.VisitMember(node);

                whereModel.Sql.Append(member.Express);
            }
            else  if(node.Expression is ConstantExpression)
            {
                var expression = node.Expression;

                    object container = ((ConstantExpression)expression).Value;
                    var member = node.Member;
                    if (member is FieldInfo)
                    {
                        object value = ((FieldInfo)member).GetValue(container);
                        return Expression.Constant(value);
                    }
                    if (member is PropertyInfo)
                    {
                        object value = ((PropertyInfo)member).GetValue(container, null);
                        return Expression.Constant(value);
                    }
                
            }

            //Result.Prefix = member.Prefix;
            //Result.Sql.Append(member.WhereExpression);
            //Sql.Append(node);

            //model.Sql.Append(node);


            return base.VisitMember(node);
        }


        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.VisitLambda(node);
        }


        protected override Expression VisitNew(NewExpression node)
        {

           

            return base.VisitNew(node);
        }

     


        protected override Expression VisitParameter(ParameterExpression node)
        {

            return base.VisitParameter(node);
        }


      


      
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }



       
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw null;
        }

       
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw null;
        }

       
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw null;
        }


       
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw null;
        }

       
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw null;
        }

       
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw null;
        }

      
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw null;
        }

      
        protected override Expression VisitExtension(Expression node)
        {
            throw null;
        }

       
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw null;
        }

     
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw null;
        }

      
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw null;
        }

       
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw null;
        }

       
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        {
            throw null;
        }


     
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw null;
        }

       
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw null;
        }



      
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return base.VisitMemberAssignment(node);
        }

      
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return base.VisitMemberBinding(node);
        }

     
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

       
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return base.VisitMemberListBinding(node);
        }

       
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }

        
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
            //throw null;
        }


       
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw null;
        }



       
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw null;
        }

       
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw null;
        }

        
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw null;
        }

       
        protected override Expression VisitTry(TryExpression node)
        {
            throw null;
        }


        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw null;
        }

    }
}
