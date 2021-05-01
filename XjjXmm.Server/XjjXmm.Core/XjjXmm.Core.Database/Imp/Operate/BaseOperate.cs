using System.Collections.Generic;
using System.Data;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Operate
{
    public class BaseOperate
    {
        private readonly DbInfo _dbInfo;
        

        //protected Dictionary<string, object> SqlParameter;

        protected readonly ProviderModel _providerModel;

        private int start = 0;

       

        public BaseOperate(DbInfo dbInfo) : this(dbInfo, new Dictionary<string, object>())
        {
           
        }


        public BaseOperate(DbInfo dbInfo, Dictionary<string, object> SqlParameter)
        {
            _dbInfo = dbInfo;


            _providerModel = new ProviderModel(dbInfo, SqlParameter, start);
        }


    }
}
