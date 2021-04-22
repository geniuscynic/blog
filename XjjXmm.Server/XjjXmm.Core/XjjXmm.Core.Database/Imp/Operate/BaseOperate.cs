using System.Collections.Generic;
using System.Data;
using DoCare.Zkzx.Core.Database.Utility;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public class BaseOperate
    {
        protected readonly IDbConnection Connection;
        
        //protected string DbPrefix;

        //protected Dictionary<string, object> SqlParameter;

        protected readonly ProviderModel _providerModel;

        private int start = 0;

        public Aop Aop { get; set; }

        public BaseOperate(IDbConnection connection):this(connection, new Dictionary<string, object>())
        {
            Connection = connection;

           
        }


        public BaseOperate(IDbConnection connection, Dictionary<string, object> SqlParameter)
        {
            //this.SqlParameter = SqlParameter;

            Connection = connection;
            
            //DbPrefix = DatabaseFactory.GetStatementPrefix(connection);

            var dbPrefix = DatabaseFactory.GetStatementPrefix(connection);

            _providerModel = new ProviderModel(dbPrefix, SqlParameter, start);
        }

       
    }
}
