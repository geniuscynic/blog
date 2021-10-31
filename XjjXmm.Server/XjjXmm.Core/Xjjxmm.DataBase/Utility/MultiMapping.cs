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
using Xjjxmm.DataBase.Utility;
using XjjXmm.DataBase.Utility;

namespace Xjjxmm.DataBase.Utility
{
    internal interface IMapping<T>
    {
        string MainKey { get; }
        string SubKey { get; }

        string LeftKey => "";
        string RightKey => "";

        T Mapping(T t, IEnumerable<object> t2);
    }

    public class MappingOneToOneEntity<T1, T2> : IMapping<T1>
    {
        private readonly Func<T1, T2, T1> _mappingFunc;

        public MappingOneToOneEntity(Expression<Func<T1, string>> predicateMain, Expression<Func<T2, string>> predicateSub, Func<T1, T2, T1> mappingFunc)
        {
            _mappingFunc = mappingFunc;
            // this.MainClassKeyPredicate = predicateMain;
            //this.SubClassPredicate = predicateSub;
            // this.MappingFunc = mappingFunc;

            var provider = new SplitOnProvider();
            provider.Visit(predicateMain);

            MainKey = provider.SelectFields.Select(t => t.Parameter).First();

            provider = new SplitOnProvider();
            provider.Visit(predicateSub);
            SubKey = provider.SelectFields.Select(t => t.ColumnName).First();


            //mappingFunc.Compile().in
        }

        public string MainKey { get; }
        public string SubKey { get; }

        public T1 Mapping(T1 t, IEnumerable<object> t2)
        {
            var res = (T2)t2.FirstOrDefault();

            //var tmp = (IEnumerable<T2>)t2;
            return _mappingFunc(t, res);
        }
    }

    public class MappingOneToManyEntity<T1, T2> : IMapping<T1>
    {
        private readonly Func<T1, IEnumerable<T2>, T1> _mappingFunc;

        public MappingOneToManyEntity(Expression<Func<T1, string>> predicateMain, Expression<Func<T2, string>> predicateSub, Func<T1, IEnumerable<T2>, T1> mappingFunc)
        {
            _mappingFunc = mappingFunc;
            // this.MainClassKeyPredicate = predicateMain;
            //this.SubClassPredicate = predicateSub;
            // this.MappingFunc = mappingFunc;

            var provider = new SplitOnProvider();
            provider.Visit(predicateMain);

            MainKey = provider.SelectFields.Select(t => t.Parameter).First();

            provider = new SplitOnProvider();
            provider.Visit(predicateSub);
            SubKey = provider.SelectFields.Select(t => t.ColumnName).First();


            //mappingFunc.Compile().in
        }

        public string MainKey { get; }
        public string SubKey { get; }

        public T1 Mapping(T1 t, IEnumerable<object> t2)
        {
            var res = new List<T2>();
            foreach (var o in t2)
            {
                res.Add((T2)o);
            }

            //var tmp = (IEnumerable<T2>)t2;
            return _mappingFunc(t, res);
        }
    }


    public class MappingManyToManyEntity<T1, T2, T3> : IMapping<T1>
    {
        private readonly Func<T1, IEnumerable<T2>, T1> _mappingFunc;

        public MappingManyToManyEntity(
            Expression<Func<T1, string>> predicateMain,
            Expression<Func<T2, string>> predicateLeft,
            Expression<Func<T2, string>> predicateRight,
            Expression<Func<T3, string>> predicateSub,
            Func<T1, IEnumerable<T2>, T1> mappingFunc)
        {
            _mappingFunc = mappingFunc;
            // this.MainClassKeyPredicate = predicateMain;
            //this.SubClassPredicate = predicateSub;
            // this.MappingFunc = mappingFunc;

            var provider = new SplitOnProvider();
            provider.Visit(predicateMain);

            MainKey = provider.SelectFields.Select(t => t.Parameter).First();

            provider = new SplitOnProvider();
            provider.Visit(predicateSub);
            SubKey = provider.SelectFields.Select(t => t.ColumnName).First();


            provider = new SplitOnProvider();
            provider.Visit(predicateLeft);
            LeftKey = provider.SelectFields.Select(t => t.ColumnName).First();

            provider = new SplitOnProvider();
            provider.Visit(predicateRight);
            RightKey = provider.SelectFields.Select(t => t.ColumnName).First();
            //mappingFunc.Compile().in
        }

        public string MainKey { get; }
        public string SubKey { get; }

        public string LeftKey { get; }
        public string RightKey { get; }

        public T1 Mapping(T1 t, IEnumerable<object> t2)
        {
            var res = new List<T2>();
            foreach (var o in t2)
            {
                res.Add((T2)o);
            }

            //var tmp = (IEnumerable<T2>)t2;
            return _mappingFunc(t, res);
        }
    }

    internal class MappingInfo<T>
    {
        internal IMapping<T> MappingMeta { get; set; }

        internal PropertyInfo PropertyInfo { get; set; }

        internal StringBuilder Sql { get; set; }

        internal IQueryableProvider Provider { get; set; }

        internal Type Type { get; set; }

        internal IEnumerable<object> Result { get; set; }
    }
    internal class MappingHelper<T>
    {

        private readonly IQueryableProvider _provider;
        private List<MappingInfo<T>> mappingInfos = new List<MappingInfo<T>>();

        private List<Action<List<T>>> actions = new List<Action<List<T>>>();

        public MappingHelper(IQueryableProvider provider)
        {
            _provider = provider;

        }


        private string GetMainKey(Expression predicate)
        {
            var visitprovider = new SplitOnProvider();
            visitprovider.Visit(predicate);
            var mainKey = visitprovider.SelectFields.Select(t => t.Parameter).First();

            return mainKey;
        }

        private string GetSubKey(Expression predicate)
        {
            var visitprovider = new SplitOnProvider();
            visitprovider.Visit(predicate);
            var mainKey = visitprovider.SelectFields.Select(t => t.ColumnName).First();

            return mainKey;
        }

        private StringBuilder GetMainKeySql(string mainKey, List<T> entities)
        {
            var propertyInfo = typeof(T).GetProperty(mainKey);

            var sql = new StringBuilder();
            foreach (var entity in entities)
            {
                sql.Append($"'{propertyInfo.GetValue(entity)}',");
            }

            sql.Remove(sql.Length - 1, 1);

            return sql;
        }

        private async Task<IEnumerable<T2>> Query<T2>(string subKey, StringBuilder sql)
        {
            var subProvider = (IQueryableProvider)_provider.Clone();
            subProvider.Where($"{subKey} in ({sql})");


            return await subProvider.CreateReaderableCommand<T2>().ExecuteQuery();
        }

        public void AddMapping<T2>(Expression<Func<T, T2>> mapperObject, Expression<Func<T, object>> predicateMain,
            Expression<Func<T2, object>> predicateSub)
        {
            Action<List<T>> action = async entities =>
            {

                var mainKey = GetMainKey(predicateMain);

                var subKey = GetSubKey(predicateSub);
                var subKey2 = GetMainKey(predicateSub);

                var mapperObjectKey = GetMainKey(mapperObject);


                var sql = GetMainKeySql(mainKey, entities);

                var subs = await Query<T2>(subKey, sql);

                //var func = mapperObject.Compile();

                var mainPropertyInfo = typeof(T).GetProperty(mainKey);
                var subPropertyInfo = typeof(T2).GetProperty(subKey2);
                var mapPropertyInfo = typeof(T).GetProperty(mapperObjectKey);
                entities.ForEach(entity =>
                {
                    //var obj = func(entity);


                    var mainId = mainPropertyInfo.GetValue(entity).ToString();

                    var sub = subs.FirstOrDefault(t => subPropertyInfo.GetValue(t).ToString() == mainId);
                    //obj = sub;

                    mapPropertyInfo.SetValue(entity, sub);
                    //foreach (var sub in subs)
                    //{
                    //    subPropertyInfo.
                    //}

                    //subs.FirstOrDefault(t=>t.)
                });

            };

            actions.Add(action);


        }
        public void AddMapping<T2>(Expression<Func<T, IEnumerable<T2>>> mapperObject, Expression<Func<T, object>> predicateMain,
            Expression<Func<T2, object>> predicateSub)
        {
            Action<List<T>> action = async entities =>
            {

                var mainKey = GetMainKey(predicateMain);

                var subKey = GetSubKey(predicateSub);
                var subKey2 = GetMainKey(predicateSub);

                var mapperObjectKey = GetMainKey(mapperObject);


                var sql = GetMainKeySql(mainKey, entities);

                var subs = await Query<T2>(subKey, sql);

                //var func = mapperObject.Compile();

                var mainPropertyInfo = typeof(T).GetProperty(mainKey);
                var subPropertyInfo = typeof(T2).GetProperty(subKey2);
                var mapPropertyInfo = typeof(T).GetProperty(mapperObjectKey);
                entities.ForEach(entity =>
                {
                    //var obj = func(entity);


                    var mainId = mainPropertyInfo.GetValue(entity).ToString();

                    var sub = subs.Where(t => subPropertyInfo.GetValue(t).ToString() == mainId);
                    //obj = sub;

                    mapPropertyInfo.SetValue(entity, sub);
                    //foreach (var sub in subs)
                    //{
                    //    subPropertyInfo.
                    //}

                    //subs.FirstOrDefault(t=>t.)
                });

            };

            actions.Add(action);


        }


        public void AddMapping<T2, T3>(
            Expression<Func<T, IEnumerable<T3>>> mapperObject,
            Expression<Func<T, object>> predicateMain,
            Expression<Func<T2, object>> predicateLeft,
            Expression<Func<T2, object>> predicateRight,
            Expression<Func<T3, object>> predicateSub) where T3:new()
        {
            Action<List<T>> action = async entities =>
            {
                var mapperObjectKey = GetMainKey(mapperObject);

                var mainKey = GetMainKey(predicateMain);

                var leftKey = GetSubKey(predicateLeft);
                var leftKey2 = GetMainKey(predicateLeft);
                var rightKey = GetSubKey(predicateRight);

                var subKey = GetSubKey(predicateSub);
                var subKey2 = GetMainKey(predicateSub);

                var sql1 = GetMainKeySql(mainKey, entities);

                var subProvider = (IQueryableProvider)_provider.Clone();
                //var sql = "select * from t3 a join t2 b on t3.id = t2.id where t2.id in ('')";

                var sql = new StringBuilder();
                sql.Append($"select t2.{leftKey2} as {leftKey},");

                var (t2TableName, _) = ProviderHelper.GetMetas(typeof(T2));

                var (t3TableName, properties) = ProviderHelper.GetMetas(typeof(T3));
                foreach (var property in properties)
                {
                    sql.Append($"t3.{property.ColumnName} as {property.Parameter},");
                }

                sql.Remove(sql.Length - 1, 1);

                sql.Append($" from {t3TableName} t3 join {t2TableName} t2 on t3.{subKey} = t2.{rightKey} ");
                sql.Append($"where t2.{leftKey} in ({sql1});");
                //subProvider.Join<T2, T3>("t", predicateMap);
                //subProvider.Join<T,T2>("a",);
                //subProvider.Where($"{subKey} in ({sql})");


                                                

                var subs = subProvider.ExecuteQuery<dynamic>(sql).Result.ToList();
                var subPropertyInfo = typeof(T3);

              
                //var subresult = new List<IDictionary<string, object>>();

                var subresult = subs.Select(x =>
                {
                    var tmp = (IDictionary<string, object>)x;
                    var t3 = new T3();
                    
                    foreach (var tmpKey in tmp.Keys)
                    {
                        if(tmpKey == leftKey) continue;

                        subPropertyInfo.GetProperty(tmpKey).SetValue(t3, tmp[tmpKey]); 
                    }


                    var resultSub = new Dictionary<string, T3>();
                    resultSub.Add(tmp[leftKey].ToString(), t3);

                    return resultSub;


                }).ToList();
              
                                //foreach (var sub in subs)
                                //{
                                //    var tmp = (IDictionary<string, object>) sub;
                                //    subresult.Add(tmp);
                                //}

                                var t3Property = subresult.First().GetType();
                                //var subs = await Query<T2>(subKey, sql);

                                //var func = mapperObject.Compile();

                                var mainPropertyInfo = typeof(T).GetProperty(mainKey);
               
                var mapPropertyInfo = typeof(T).GetProperty(mapperObjectKey);
                entities.ForEach(entity =>
                {
                    //var obj = func(entity);

                    try
                    {
                        var mainId = mainPropertyInfo.GetValue(entity).ToString();

                       
                        var sub = subresult.Where(t =>
                           t.ContainsKey(mainId)).SelectMany(t=>t.Values).ToList();
                        //obj = sub;
                       
                            mapPropertyInfo.SetValue(entity, sub);
                       
                       
                    }
                    catch (Exception ex)
                    {
                        var a = ex;
                    }
                 
                    //foreach (var sub in subs)
                    //{
                    //    subPropertyInfo.
                    //}

                    //subs.FirstOrDefault(t=>t.)
                });

            };

            actions.Add(action);


        }

        public MappingHelper<T> AddMapping<T2>(MappingOneToOneEntity<T, T2> mappingEntity)
        {

            mappingInfos.Add(new MappingInfo<T>()
            {
                MappingMeta = mappingEntity,
                PropertyInfo = typeof(T).GetProperty(mappingEntity.MainKey),
                Sql = new StringBuilder(),
                Provider = (IQueryableProvider)_provider.Clone(),
                Type = typeof(T2)

            });

            return this;
        }

        public MappingHelper<T> AddMapping<T2>(MappingOneToManyEntity<T, T2> mappingEntity)
        {
            mappingInfos.Add(new MappingInfo<T>()
            {
                MappingMeta = mappingEntity,
                PropertyInfo = typeof(T).GetProperty(mappingEntity.MainKey),
                Sql = new StringBuilder(),
                Provider = (IQueryableProvider)_provider.Clone(),
                Type = typeof(T2)

            });

            return this;
        }

        public MappingHelper<T> AddMapping<T2, T3>(MappingManyToManyEntity<T, T2, T3> mappingEntity)
        {
            mappingInfos.Add(new MappingInfo<T>()
            {
                MappingMeta = mappingEntity,
                PropertyInfo = typeof(T).GetProperty(mappingEntity.MainKey),
                Sql = new StringBuilder(),
                Provider = (IQueryableProvider)_provider.Clone(),
                Type = typeof(T3)
            });
            //_provider.Join<T2,T3>("p",);
            return this;
        }

        public void Exec2(List<T> res)
        {
            actions.ForEach(t =>
            {
                t(res);
            });
        }

        public async Task<IEnumerable<T>> Exec()
        {
            var resultsList = (await _provider.CreateReaderableCommand<T>().ExecuteQuery()).ToList();

            foreach (var res in resultsList)
            {
                foreach (var mappingInfo in mappingInfos)
                {
                    mappingInfo.Sql.Append($"'{mappingInfo.PropertyInfo.GetValue(res)}',");
                }
            }

            foreach (var mappingInfo in mappingInfos)
            {
                mappingInfo.Sql.Remove(mappingInfo.Sql.Length - 1, 1);

                mappingInfo.Provider.Where($"{mappingInfo.MappingMeta.SubKey} in ({mappingInfo.Sql})");


                var tmp = await mappingInfo.Provider.ExecuteQuery(mappingInfo.Type);

                mappingInfo.Result = tmp;

            }

            foreach (var result in resultsList)
            {
                //var tmp = result;

                foreach (var mappingInfo in mappingInfos)
                {
                    //tmp = 
                    mappingInfo.MappingMeta.Mapping(result, mappingInfo.Result);

                }
            }

            return resultsList;
        }


    }



}
