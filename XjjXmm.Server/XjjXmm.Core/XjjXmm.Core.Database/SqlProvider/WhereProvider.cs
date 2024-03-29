﻿using System.Collections.Generic;
using System.Linq.Expressions;
using DoCare.Zkzx.Core.Database.Imp.Operate.MySqlOperate;
using DoCare.Zkzx.Core.Database.Imp.Operate.OracleOperate;
using DoCare.Zkzx.Core.Database.Imp.Operate.SqlOperate;
using DoCare.Zkzx.Core.Database.Interface.Operate;
using DoCare.Zkzx.Core.Database.Utility;


namespace DoCare.Zkzx.Core.Database.SqlProvider
{

    internal abstract class WhereProvider : ExpressionVisitor
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
            {ExpressionType.NotEqual, " <> "},
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
            whereModel.Sql.Append($"{_providerModel.DbInfo.StatementPrefix}p{_providerModel.Start}");

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

       

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
           // var expression = node.Expression as MethodCallExpression;

            var sql = ProviderHelper.VisitSqlFuc(node, CreateSqlFunVisit());

            if (sql == null)
            {
                var value = Expression.Lambda(node).Compile().DynamicInvoke();
                AddConstant(value);
                return Expression.Constant(value);
            }
            else
            {
                whereModel.Sql.Append(sql);
            }
            //var sqlFunc = DatabaseFactory.CreateSqlFunc(_providerModel.DbType);

            //var member = ProviderHelper.VisitMember(node);

            //whereModel.Prefix = member.Prefix;
            
            return node;
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


        protected abstract ISqlFuncVisit CreateSqlFunVisit();

    }

    internal class MySqlWhereProvider : WhereProvider
    {
        public MySqlWhereProvider(ProviderModel providerModel) : base(providerModel)
        {
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new MySqlSqlFunc();
        }
    }

    internal class MsSqlWhereProvider : WhereProvider
    {
        public MsSqlWhereProvider(ProviderModel providerModel) : base(providerModel)
        {
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new MsSqlSqlFunc();
        }
    }

    internal class OracleWhereProvider : WhereProvider
    {
        public OracleWhereProvider(ProviderModel providerModel) : base(providerModel)
        {
        }

        protected override ISqlFuncVisit CreateSqlFunVisit()
        {
            return new OracleSqlFunc();
        }
    }
}
