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

    internal class MappingHelper<T>
    {
        private readonly IQueryableProvider _provider;

        internal List<T> resultsList;

        private List<PropertyInfo> _propertyInfos = new List<PropertyInfo>();
        private List<StringBuilder> _ids = new List<StringBuilder>();
        internal List<IQueryableProvider> cloneProviders = new List<IQueryableProvider>();

        public MappingHelper(IQueryableProvider provider)
        {
            _provider = provider;

            resultsList = _provider.CreateReaderableCommand<T>().ExecuteQuery().Result.ToList();
        }

        public MappingHelper<T> AddProperty(string mainClassKey)
        {
            _propertyInfos.Add(typeof(T).GetProperty(mainClassKey));
            _ids.Add(new StringBuilder());
            cloneProviders.Add((IQueryableProvider)_provider.Clone());

            return this;
        }

        public MappingHelper<T> BuildKey()
        {
            foreach (var res in resultsList)
            {
                for (var i = 0; i < _propertyInfos.Count; i++)
                {
                    _ids[i].Append($"'{_propertyInfos[i].GetValue(res)}',");
                }
            }

            foreach (var sb in _ids)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return this;
        }

        public MappingHelper<T> BuilderProvider(params string[] subClassKeys)
        {
            for (var i = 0; i < subClassKeys.Length; i++)
            {
                cloneProviders[i].Where($"{subClassKeys[i]} in ({_ids[i]})");
            }

            return this;
        }


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
