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

		T Mapping(T t, dynamic t2);
	}

	public class MappingEntity<T1, T2> : IMapping<T1>
	{
		private readonly Func<T1, IEnumerable<T2>, T1> _mappingFunc;

		public MappingEntity(Expression<Func<T1, string>> predicateMain, Expression<Func<T2, string>> predicateSub, Func<T1, IEnumerable<T2>, T1> mappingFunc)
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

		public T1 Mapping(T1 t, dynamic t2)
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

		internal object Result { get; set; }
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

		public MappingHelper<T> AddMapping<T2>(MappingEntity<T, T2> mappingEntity)
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
