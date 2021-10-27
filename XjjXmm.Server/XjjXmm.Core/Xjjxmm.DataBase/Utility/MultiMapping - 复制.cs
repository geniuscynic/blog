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

		// internal List<T> resultsList;

		// private List<PropertyInfo> _propertyInfos = new List<PropertyInfo>();
		// private List<StringBuilder> _ids = new List<StringBuilder>();
		// internal List<IQueryableProvider> cloneProviders = new List<IQueryableProvider>();

		//List<Delegate> mappingFuncs = new List<Delegate>();
		//List<string> subKeyList = new List<string>();

		//List<T> resultsList = new List<T>();
		//List<IEnumerable<object>> otherResult = new List<IEnumerable<object>>();
		//List<Type> types = new List<Type>();

		// List<IMapping<T>> mappings = new List<IMapping<T>>();

		public MappingHelper(IQueryableProvider provider)
		{
			_provider = provider;

		}

        public void AddMapping<T2>(Expression<Func<T, T2>> mapperObject, Expression<Func<T, object>> predicateMain,
            Expression<Func<T2, object>> predicateSub)
        {
            Action<List<T>> action = entities =>
            {
                var subProvider = (IQueryableProvider) _provider.Clone();

                var visitprovider = new SplitOnProvider();
                visitprovider.Visit(predicateMain);
                var mainKey = visitprovider.SelectFields.Select(t => t.Parameter).First();

                var mainProperty = typeof(T).GetProperty(mainKey);

                var sql = new StringBuilder();
                foreach (var entity in entities)
                {
                    sql.Append($"'{mainProperty.GetValue(entity)}',");
                }

                sql.Remove(sql.Length - 1, 1);

                //subProvider.Where($"{MinKey} in ({mappingInfo.Sql})");


               // var tmp = await mappingInfo.Provider.ExecuteQuery<T2>();

                var res = mapperObject.Compile();
                res()
            };
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

        public MappingHelper<T> AddMapping<T2,T3>(MappingManyToManyEntity<T, T2, T3> mappingEntity)
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
