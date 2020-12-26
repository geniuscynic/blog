using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp1
{
    public class MyQueryable<T> : IQueryable<T>
    {
        private readonly Expression _expression;
        private readonly IQueryProvider _provider;

        public MyQueryable(Expression expression, IQueryProvider provider)
        { 
            _expression = expression;
            _provider = provider;
        }

        public MyQueryable(IQueryProvider provider)
        {
            _provider = provider;

            _expression = Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) Provider.Execute<T>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public IQueryProvider Provider => _provider;
    }
}
