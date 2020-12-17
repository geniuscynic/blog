﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Queryable<T>
    {
        protected List<Expression> _whereExpressionList = new List<Expression>();

        public Queryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            _whereExpressionList.Add(predicate);
            return this;
        }

        public string build()
        {
            _whereExpressionList.ForEach(t =>
            {
                var vistor = new MyExpressionVisitor(t);
                vistor.Visit()
            });
             
        }
    }
}
