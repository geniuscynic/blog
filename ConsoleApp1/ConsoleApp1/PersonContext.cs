using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp1
{
    public class PersonContext : IQueryable<Person>, IQueryProvider
    {
        #region IQueryable Members

        Type IQueryable.ElementType
        {
            get { return typeof(Person); }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return Expression.Constant(this); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return this; }
        }

        #endregion


        #region IEnumerable<Person> Members

        IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
        {
            return (this as IQueryable).Provider.Execute<IEnumerator<Person>>(_expression);
        }

        private IList<Person> _person = new List<Person>();
        private Expression _expression = null;

        #endregion


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Person>)(this as IQueryable).GetEnumerator();
        }


        private void ProcessExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Equal)
            {
                ProcessEqualResult((BinaryExpression)expression);
            }
            if (expression.NodeType == ExpressionType.LessThan)
            {
                _person = GetPersons();

                var query = from p in _person
                            where p.Age < (int)GetValue((BinaryExpression)expression)
                            select p;
                _person = query.ToList<Person>();
            }
            if (expression is UnaryExpression)
            {
                UnaryExpression uExp = expression as UnaryExpression;
                ProcessExpression(uExp.Operand);
            }
            else if (expression is LambdaExpression)
            {
                ProcessExpression(((LambdaExpression)expression).Body);
            }
            else if (expression is ParameterExpression)
            {
                if (((ParameterExpression)expression).Type == typeof(Person))
                {
                    _person = GetPersons();
                }
            }
        }

        private void ProcessEqualResult(BinaryExpression expression)
        {
            if (expression.Right.NodeType == ExpressionType.Constant)
            {
                string name = (String)((ConstantExpression)expression.Right).Value;
                ProceesItem(name);
            }
        }


        private void ProceesItem(string name)
        {
            IList<Person> filtered = new List<Person>();

            foreach (Person person in GetPersons())
            {
                if (string.Compare(person.Name, name, true) == 0)
                {
                    filtered.Add(person);
                }
            }
            _person = filtered;
        }


        private object GetValue(BinaryExpression expression)
        {
            if (expression.Right.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)expression.Right).Value;
            }
            return null;
        }

        IList<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person { ID = 1, Name="Mehfuz Hossain", Age=27},
                new Person { ID = 2, Name="Json Born", Age=30},
                new Person { ID = 3, Name="John Doe", Age=52}
            };

        }

        #endregion


        #region IQueryProvider Members

        IQueryable<S> IQueryProvider.CreateQuery<S>(System.Linq.Expressions.Expression expression)
        {
            if (typeof(S) != typeof(Person))
                throw new Exception("Only " + typeof(Person).FullName + " objects are supported.");

            this._expression = expression;

            return (IQueryable<S>)this;
        }

        IQueryable IQueryProvider.CreateQuery(System.Linq.Expressions.Expression expression)
        {
            return (IQueryable<Person>)(this as IQueryProvider).CreateQuery<Person>(expression);
        }

        TResult IQueryProvider.Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            MethodCallExpression methodcall = _expression as MethodCallExpression;

            foreach (var param in methodcall.Arguments)
            {
                ProcessExpression(param);
            }
            return (TResult)_person.GetEnumerator();
        }

        object IQueryProvider.Execute(System.Linq.Expressions.Expression expression)
        {
            return (this as IQueryProvider).Execute<IEnumerator<Person>>(expression);
        }

        #endregion
    }
}
