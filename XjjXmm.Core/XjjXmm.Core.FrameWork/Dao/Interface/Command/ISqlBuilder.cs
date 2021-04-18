namespace DoCare.Extension.Dao.Interface.Command
{
    interface ISqlBuilder
    {
        string Build(bool ignorePrefix = true);
    }
}
