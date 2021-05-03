using System;
using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.MsSql
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
