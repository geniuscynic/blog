using System;
using System.Collections.Generic;
using System.Globalization;
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

    public delegate T1 MappingFunction<T1, in T2>(T1 p1, T2 p2);
    internal class MappingHelper2<T>
    {
          

        private readonly IQueryableProvider _provider;

       // internal List<T> resultsList;

        private List<PropertyInfo> _propertyInfos = new List<PropertyInfo>();
        private List<StringBuilder> _ids = new List<StringBuilder>();
        internal List<IQueryableProvider> cloneProviders = new List<IQueryableProvider>();

        List<Delegate> mappingFuncs = new List<Delegate>();
        List<string> subKeyList = new List<string>();
        
        List<T> resultsList = new List<T>();
        List<IEnumerable<object>> otherResult = new List<IEnumerable<object>>();
        List<Type> types = new List<Type>();

        public MappingHelper2(IQueryableProvider provider)
        {
            _provider = provider;

        }

        public MappingHelper2<T> AddMapping<T2>(MappingEntity<T, T2> mappingEntity)
        {
            _propertyInfos.Add(typeof(T).GetProperty(mappingEntity.MainClassKey));
            _ids.Add(new StringBuilder());
            cloneProviders.Add((IQueryableProvider)_provider.Clone());

            subKeyList.Add(mappingEntity.SubClassKey);

            mappingFuncs.Add(mappingEntity.MapExpression);
            types.Add(typeof(T2));
            return this;
        }

        private async Task Build()
        {
            resultsList = (await _provider.CreateReaderableCommand<T>().ExecuteQuery()).ToList();

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


            for (var i = 0; i < subKeyList.Count; i++)
            {
                cloneProviders[i].Where($"{subKeyList[i]} in ({_ids[i]})");
            }
        }

        public async Task<IEnumerable<T>> Exec()
        {
            await this.Build();
            //foreach (var queryableProvider in cloneProviders)
            //{
            //    otherResult.Add(await queryableProvider.CreateReaderableCommand<dynamic>().ExecuteQuery());
            //}

            for (var i = 0; i < cloneProviders.Count; i++)
            {
                // var tmp1 = await cloneProviders[i].CreateReaderableCommand<object>(types[i]).ExecuteQuery(types[i]);

                var tmp1 = await cloneProviders[i].ExecuteQuery(types[i]);
               // tmp1.Select(m => (T2)m);
                //var tmp2 = new List<dynamic>();


                //var top = tmp1.GetType().GetProperties();
                
                //foreach (var o in tmp1)
                //{
                //    var p = Activator.CreateInstance(types[i]);
                //    foreach (var propertyInfo in types[i].GetProperties())
                //    {
                //       var pp = top.FirstOrDefault(t => t.Name.ToLower() == propertyInfo.Name.ToLower());
                //       var val = pp.GetValue(o);
                //        propertyInfo.SetValue(p, val);
                //    }
                //    tmp2.Add(GetValue(types[i], o));
                //}

                otherResult.Add(tmp1);
            }

            foreach (var result in resultsList)
            {
                var tmp = result;
                for (var i = 0; i < mappingFuncs.Count; i++)
                {
                    var res2 = otherResult[i]; //GetValue(types[i], otherResult[i]);

                    //var top = tmp1.GetType().GetProperties();

                    //foreach (var o in res2)
                    //{
                    //    var p = Activator.CreateInstance(types[i]);
                    //    foreach (var propertyInfo in types[i].GetProperties())
                    //    {
                    //        var pp = propertyInfo;
                    //        var val = pp.GetValue(o);
                    //        propertyInfo.SetValue(p, val);
                    //    }
                    //    tmp2.Add(GetValue(types[i], o));
                    //}

                    //(DDD)


                    var func = mappingFuncs[i];
                    //tmp = func.DynamicInvoke(tmp,  res2);

                    tmp = (T)func.Method.Invoke(func.Target, new object[] { tmp, res2 });
                }
                
            }

            

            

            return resultsList;
        }

        private static object GetValue(Type effectiveType, object val)
        {
            if (val == null && (!effectiveType.IsValueType || Nullable.GetUnderlyingType(effectiveType) != null))
            {
                return default;
            }
            else if (val is Array array && typeof(T).IsArray)
            {
                var elementType = typeof(T).GetElementType();
                var result = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                    result.SetValue(Convert.ChangeType(array.GetValue(i), elementType, CultureInfo.InvariantCulture), i);
                return result;
            }
            
            else
            {
                try
                {
                    var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                    return Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {

                    return default; // For the compiler - we've already thrown
                }
            }
        }




    }

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
        private MappingFunction<T1, T2> _mapExpression;

        //private string _mainClassKey;
        //private string _subClassKey;
        //  private Expression<Func<T1, string>> MainClassKeyPredicate { get; }
        //  private Expression<Func<T2, string>> SubClassPredicate { get; }
        //  private Expression<Func<T1, IEnumerable<T2>, T1>> MappingFunc { get; }

        private string _MainClassKey { get; }
        private string _SubClassKey { get; }

        public string MainClassKey => _MainClassKey;
        public string SubClassKey => _SubClassKey;

        public MappingFunction<T1, T2> MapExpression => _mapExpression;

        public MappingEntity(Expression<Func<T1, string>> predicateMain, Expression<Func<T2, string>> predicateSub, MappingFunction<T1,T2> mappingFunc)
        {
            // this.MainClassKeyPredicate = predicateMain;
            //this.SubClassPredicate = predicateSub;
            // this.MappingFunc = mappingFunc;

            var provider = new SplitOnProvider();
            provider.Visit(predicateMain);

            _MainClassKey = provider.SelectFields.Select(t => t.Parameter).First();

            provider = new SplitOnProvider();
            provider.Visit(predicateSub);
            _SubClassKey = provider.SelectFields.Select(t => t.ColumnName).First();


            _mapExpression = mappingFunc;


            //mappingFunc.Compile().in
        }


        // string IMapping.MainClassKey => _mainClassKey;

        //string IMapping.SubClassKey => _subClassKey;
    }
}
