using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.Oracle
{
    internal class OracleSqlJoinCommand : JoinCommand
    {
        public OracleSqlJoinCommand(string alias, ProviderModel providerModel) : base(alias, providerModel)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new OracleWhereProvider(providerModel);
        }
    }


}
