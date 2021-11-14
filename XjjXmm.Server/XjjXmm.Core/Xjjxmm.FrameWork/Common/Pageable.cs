using System.Collections.Generic;

namespace XjjXmm.FrameWork.Common
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class Pageable
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; } = 10;

        
        //public int Offset { set; get; }


        public string[] Sort { get; set; }

        public Direction Direction { get; set; } = Direction.ASC;

    }

    public enum Direction
    {
        ASC,
        DESC
    }

}
