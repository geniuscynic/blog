namespace XjjXmm.Authorize.Repository.Entity
{
    /// <summary>
    /// 登入viewmodel
    /// </summary>
    public class SearchUserModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Name { get; set; }

        public int PageIndex
        {
            get; set;
        }

        public int PageSize
        {
            get; set;
        }


    }

}
