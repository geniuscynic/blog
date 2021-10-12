using XjjXmm.DataBase.SqlProvider;
using XjjXmm.DataBase.Utility;

namespace XjjXmm.DataBase.Imp.Command.MySql
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
