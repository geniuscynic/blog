using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp1
{
    public class Mycontext<T> : MyQueryable<T>
    {
        public Mycontext()  : base(new MyQueryProvider())
        {
           
        }
        public Mycontext(Expression expression, IQueryProvider provider) : base(expression, provider)
        {
        }

        public Mycontext(IQueryProvider provider) : base(provider)
        {
        }
    }
}
