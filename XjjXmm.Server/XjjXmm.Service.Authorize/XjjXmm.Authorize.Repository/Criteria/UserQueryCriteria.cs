using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Repository.Criteria
{
    public class UserQueryCriteria : Pageable
    {
        public long Id { get; }


        public List<long> DeptIds { get; set; } = new List<long>();


        public string Blurry { get; set; }


        public bool Enabled { get; set; }

        public long? DeptId { get; set; }


        public List<DateTime> CreateTime { get; set; }
    }
}
