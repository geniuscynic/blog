using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.DataBase.Interface.Operate;
using XjjXmm.DataBase.SqlProvider;

namespace Xjjxmm.DataBase.Utility
{
    //internal interface IMapping
    //{
    //    string MainClassKey { get; }
    //    string SubClassKey { get; }

    //    LambdaExpression MapExpression { get; }
    //}

    internal abstract class Mapping<T> {
        private readonly IQueryableProvider _provider;

        private StringBuilder ids1 = new StringBuilder();
        public Mapping(IQueryableProvider provider)
        {
            _provider = provider;
        }

        public PropertyInfo GetMainKeysProperty(string mainClassKey)
        {
            return typeof(T).GetProperty(mainClassKey);
        }

        public void BuildKey(T res, PropertyInfo property)
        {
            ids1.Append($"'{property.GetValue(res)}',");
        }

        public StringBuilder GetIds()
        {
            ids1.Remove(ids1.Length - 1, 1);
            return ids1;
        }

        public abstract IEnumerable<T2> GetSubEnumerable<T2>(string subKey);
  
    }


    public class MappingEntity<T1, T2>
    {
        private Func<T1, IEnumerable<T2>, T1> _mapExpression;

        //private string _mainClassKey;
        //private string _subClassKey;
        //  private Expression<Func<T1, string>> MainClassKeyPredicate { get; }
        //  private Expression<Func<T2, string>> SubClassPredicate { get; }
        //  private Expression<Func<T1, IEnumerable<T2>, T1>> MappingFunc { get; }

        private string _MainClassKey { get; }
        private string _SubClassKey { get; }

        public string MainClassKey => _MainClassKey;
        public string SubClassKey => _SubClassKey;

        public Func<T1, IEnumerable<T2>, T1> MapExpression => _mapExpression;





        public MappingEntity(Expression<Func<T1, string>> predicateMain, Expression<Func<T2, string>> predicateSub, Func<T1, IEnumerable<T2>, T1> mappingFunc)
        {
            // this.MainClassKeyPredicate = predicateMain;
            //this.SubClassPredicate = predicateSub;
            // this.MappingFunc = mappingFunc;

            var provider = new SplitOnProvider();
            provider.Visit(predicateMain);

            _MainClassKey = provider.SelectFields.Select(t => t.Parameter).First();

            provider.Visit(predicateSub);
            _SubClassKey = provider.SelectFields.Select(t => t.ColumnName).First();


            _mapExpression = mappingFunc;


            //mappingFunc.Compile().in
        }


        // string IMapping.MainClassKey => _mainClassKey;

        //string IMapping.SubClassKey => _subClassKey;
    }
}
