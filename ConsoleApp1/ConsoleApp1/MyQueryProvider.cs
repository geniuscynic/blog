using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp1
{
    public class MyQueryProvider : IQueryProvider
    {
        private StringBuilder _whereSql = new StringBuilder();
        private StringBuilder _selectSql = new StringBuilder();

        public IQueryable CreateQuery(Expression expression)
        {
            return (IQueryable)Activator.CreateInstance(
                typeof(MyQueryable<>).MakeGenericType(expression.Type),
                new object[] { expression, this });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MyQueryable<TElement>(expression, this);
        }

        public object Execute(Expression expression)
        {
            return excute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)excute(expression);
        }


        /*提供抽象方法，交给下层实现*/
        protected object excute(Expression expression)
        {
           

            ProcessExpression(expression);
           
            return "";
        }


        private void ProcessExpression(Expression expression)
        {

            switch (expression)
            {
                case MethodCallExpression express:

                    var name = express.Method.Name;
                    Console.WriteLine(name);
                    foreach (var param in express.Arguments)
                    {
                        

                        if (param is MethodCallExpression)
                        {
                            ProcessExpression(param);

                            _whereSql.Append(" and ");
                        }
                        else if (name == "Select")
                        {
                            ProcessSelectExpression(param);
                        }
                    }

                    break;

                case BinaryExpression express:

                    
                    ProcessExpression(express.Left);

                    switch (express.NodeType)
                    {
                        case ExpressionType.Equal:
                            _whereSql.Append(" = ");
                            break;

                        case ExpressionType.GreaterThan:
                            _whereSql.Append(" > ");
                            break;

                        case ExpressionType.GreaterThanOrEqual:
                            _whereSql.Append(" >= ");
                            break;

                        case ExpressionType.LessThan:
                            _whereSql.Append(" < ");
                            break;

                        case ExpressionType.LessThanOrEqual:
                            _whereSql.Append(" <= ");
                            break;

                        case ExpressionType.AndAlso:
                            _whereSql.Append(" and ");
                            break;
                    }

                    ProcessExpression(express.Right);
                    
                    
                    break;

                case MemberExpression express:
                    _whereSql.Append(express.ToString());


                   

                    break;

                case UnaryExpression express:
                    ProcessExpression(express.Operand);

                    break;

                case ConstantExpression express:
                    if (express.Type == typeof(int)) {
                            _whereSql.Append(express.Value);

                    }
                    else if (express.Type == typeof(string)) {
                        _whereSql.Append($"'{express.Value}'");

                    }

                    break;
                case ConditionalExpression express:

                    var ca = "";
                    break;
                case LambdaExpression express:

                    ProcessExpression(express.Body);
                    break;
                case ParameterExpression express:

                    var e = "";
                    break;
            }

        }

        private void ProcessSelectExpression(Expression expression)
        {
            switch (expression)
            {
                case MethodCallExpression express:

                    
                    break;

                case BinaryExpression express:



                    break;

                case MemberExpression express:
                    //_whereSql.Append(express.ToString());

                    _selectSql.Append($"{express}, ");


                    break;

                case UnaryExpression express:
                    ProcessSelectExpression(express.Operand);

                    break;

                case ConstantExpression express:
                    //if (express.Type == typeof(int))
                    //{
                    //    _whereSql.Append(express.Value);

                    //}
                    //else if (express.Type == typeof(string))
                    //{
                    //    _whereSql.Append($"'{express.Value}'");

                    //}

                    break;
                case ConditionalExpression express:

                    var ca = "";
                    break;
                case LambdaExpression express:

                    ProcessSelectExpression(express.Body);
                    break;
                case ParameterExpression express:

                    var e = "";
                    break;

                case NewExpression express:
                    //var length = express.Arguments.Count;

                    foreach (var expressArgument in express.Arguments)
                    {
                        ProcessSelectExpression(expressArgument);
                    }
                    //foreach (var valueTuple in express.Arguments.Zip(express.Members))
                    //{
                    //    if (valueTuple.First is MemberExpression member)
                    //    {
                    //        _selectSql.Append($"{member.ToString()} as '{valueTuple.Second.Name}', ");

                    //    }
                    //}


                    break;
            }
        }
        //protected abstract string getQueryText();

        /*重写ToString方法*/
        //public override string ToString()
        //{
        //    return getQueryText();
        //}
    }
}
