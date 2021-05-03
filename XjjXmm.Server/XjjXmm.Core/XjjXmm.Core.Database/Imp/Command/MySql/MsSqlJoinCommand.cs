using DoCare.Zkzx.Core.Database.SqlProvider;
using DoCare.Zkzx.Core.Database.Utility;

namespace DoCare.Zkzx.Core.Database.Imp.Command.MySql
{
    internal class MySqlJoinCommand : JoinCommand
    {
        public MySqlJoinCommand(string alias, ProviderModel providerModel) : base(alias, providerModel)
        {
        }

        protected override WhereProvider CreateWhereProvider(ProviderModel providerModel)
        {
            return new MySqlWhereProvider(providerModel);
        }
    }


}
