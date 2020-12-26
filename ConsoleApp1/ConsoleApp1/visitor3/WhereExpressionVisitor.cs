using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WhereModel
    {
        public int Start { get; set; }

        public StringBuilder Sql { get; set; }  = new StringBuilder();

        //public StringBuilder ResultSql { get; set; } = new StringBuilder();

        public Dictionary<string, object> Paramters = new Dictionary<string, object>();
    }

    public class WhereExpressionVisitor : ExpressionVisitor
    {
        public WhereModel Result { get; } = new WhereModel();

        private readonly Dictionary<ExpressionType, string> _expressionTypeMapping;
        public WhereExpressionVisitor() //: this(new WhereModel())
        {
            _expressionTypeMapping = new Dictionary<ExpressionType, string>()
            {
                {ExpressionType.Equal, " = "},
                {ExpressionType.GreaterThan, " > "},
                {ExpressionType.GreaterThanOrEqual, " >= "},
                {ExpressionType.LessThan, " < "},
                {ExpressionType.LessThanOrEqual, " <= "},
                {ExpressionType.AndAlso, " and "},
            };
        }

        //public WhereExpressionVisitor(WhereModel model)
        //{
        //    this.Result = model;

        //    _expressionTypeMapping = new Dictionary<ExpressionType, string>()
        //    {
        //        {ExpressionType.Equal, " = "},
        //        {ExpressionType.GreaterThan, " > "},
        //        {ExpressionType.GreaterThanOrEqual, " >= "},
        //        {ExpressionType.LessThan, " < "},
        //        {ExpressionType.LessThanOrEqual, " <= "},
        //        {ExpressionType.AndAlso, " and "},
        //    };
        //}

        public void Run(Expression node)
        {
            if (Result.Sql.Length > 0)
            {
                Result.Sql.Append(" and ");
            }
            else
            {
                Result.Sql.Append(" where ");
            }

            Result.Sql.Append(" ( ");
            Visit(node);
            Result.Sql.Append(" ) ");
        }

        //public WhereModel GetResult()
        //{
        //    //_model.Sql.Insert(0, "(");
        //    //_model.Sql.Append(")");

        //    //if (_model.ResultSql.Length == 0)
        //    //{
        //    //    _model.ResultSql.Append(_model.Sql);
        //    //}
        //    //else
        //    //{
        //    //    _model.ResultSql.Append(" and ");
        //    //    _model.ResultSql.Append(_model.Sql);
        //    //}                                                         

        //    return Result;
        //}

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //var whereModel =  new WhereModel();
            Visit(node.Left);

            if (_expressionTypeMapping.ContainsKey(node.NodeType))
            {
                Result.Sql.Append(_expressionTypeMapping[node.NodeType]);
            }

            Visit(node.Right);

            return node;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            //memberList.Add(node.ToString());
            Result.Sql.Append($"@p{Result.Start}");

            Result.Paramters.Add($"p{Result.Start}", node.Value);

            Result.Start++;
            //model.value = node.Value;

            return base.VisitConstant(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Result.Sql.Append(node);
            //Sql.Append(node);

            //model.Sql.Append(node);


            return base.VisitMember(node);
        }
    }
}
