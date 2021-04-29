using System.Collections.Generic;
using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public class BaseOperate
    {
        private readonly DbInfo _dbClientParamter;
        

        //protected Dictionary<string, object> SqlParameter;

        protected readonly ProviderModel _providerModel;

        private int start = 0;

       

        public BaseOperate(DbInfo dbClientParamter) : this(dbClientParamter, new Dictionary<string, object>())
        {
           
        }


        public BaseOperate(DbInfo dbClientParamter, Dictionary<string, object> SqlParameter)
        {
            _dbClientParamter = dbClientParamter;


            _providerModel = new ProviderModel(dbClientParamter, SqlParameter, start);
        }


    }
}
