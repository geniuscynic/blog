namespace Permission.Model
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 删除状态
        /// </summary>
        Delete = 0,

        /// <summary>
        /// 可用状态
        /// </summary>
        Active = 1
    }


    public static class StatusExtension
    {
        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int ToInt(this Status status)
        {
            return (int) status;
        }
    }

}
