using System.Collections.Generic;

namespace DoCare.Zkzx.Core.FrameWork.Tool.Common
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int Page { get; set; } = 1;
       
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; } = 10;
        /// <summary>
        /// 返回数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => Total / PageSize + 1;
        //public int PageCount { get; set; } = 6;

    }

}
