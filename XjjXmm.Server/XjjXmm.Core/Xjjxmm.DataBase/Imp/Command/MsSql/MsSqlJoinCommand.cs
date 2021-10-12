using System;
using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.MsSql
{
    internal class MsSqlJoinCommand : JoinCommand
    {
        public MsSqlJoinCommand(string alias, ProviderModel providerModel) : base(alias, providerModel)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MySqlWhereProvider(providerModel);
        }
    }


}
