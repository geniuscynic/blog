using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyExpressionVisitor : ExpressionVisitor
    {
        //private readonly WhereExpression _expression;

        public StringBuilder SQL
        {
            get;
            set;
        }         = new StringBuilder();

        private List<string> memberList = new List<string>();
        

        public MyExpressionVisitor()
        {
            //_expression = expression;
        }
        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.BinaryExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitBinary(BinaryExpression node)
        {
            //SQL.Append($"({node.Left}");

           

            //SQL.Append($"{node.Right})");

            Visit(node.Left);
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    SQL.Append(" = ");
                    break;

                case ExpressionType.GreaterThan:
                    SQL.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    SQL.Append(" >= ");
                    break;

                case ExpressionType.LessThan:
                    SQL.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    SQL.Append(" <= ");
                    break;

                case ExpressionType.AndAlso:
                    SQL.Append(" and ");
                    break;
            };
            Visit(node.Right);
            return node;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.BlockExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.CatchBlock.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.ConditionalExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the System.Linq.Expressions.ConstantExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitConstant(ConstantExpression node)
        {
            //memberList.Add(node.ToString());
            SQL.Append(node.ToString());
            return base.VisitConstant(node);
        }

        //
        // 摘要:
        //     Visits the System.Linq.Expressions.DebugInfoExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the System.Linq.Expressions.DefaultExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.DynamicExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.ElementInit.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the extension expression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitExtension(Expression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.GotoExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.IndexExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.InvocationExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.LabelExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the System.Linq.Expressions.LabelTarget.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.WhereExpression`1.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 类型参数:
        //   T:
        //     The type of the delegate.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.VisitLambda(node);
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.ListInitExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.LoopExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitMember(MemberExpression node)
        {
            memberList.Add(node.ToString());
            SQL.Append(node);

            return base.VisitMember(node);
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberAssignment.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberBinding.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberInitExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberListBinding.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MemberMemberBinding.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.MethodCallExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
           return base.VisitMethodCall(node);
            //throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.NewExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.NewArrayExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the System.Linq.Expressions.ParameterExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.RuntimeVariablesExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.SwitchExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.SwitchCase.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.TryExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitTry(TryExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.TypeBinaryExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw null;
        }

        //
        // 摘要:
        //     Visits the children of the System.Linq.Expressions.UnaryExpression.
        //
        // 参数:
        //   node:
        //     The expression to visit.
        //
        // 返回结果:
        //     The modified expression, if it or any subexpression was modified; otherwise,
        //     returns the original expression.
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }

    }
}
