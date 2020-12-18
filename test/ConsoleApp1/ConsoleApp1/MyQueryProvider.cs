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

                    foreach (var param in express.Arguments)
                    {
                        ProcessExpression(param);
                    }

                    break;

                case BinaryExpression express:

                    
                    ProcessExpression(express.Left);

                    switch (express.NodeType)
                    {
                                           
                    }

                    ProcessExpression(express.Right);
                    
                    
                    break;

                case MemberExpression express:
                    _whereSql.Append(express.ToString());


                   

                    break;

                case UnaryExpression express:
                    ProcessExpression(express.Operand);

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
        //protected abstract string getQueryText();

        /*重写ToString方法*/
        //public override string ToString()
        //{
        //    return getQueryText();
        //}
    }
}
