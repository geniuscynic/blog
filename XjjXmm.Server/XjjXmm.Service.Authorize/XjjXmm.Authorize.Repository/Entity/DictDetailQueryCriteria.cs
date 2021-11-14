using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Repository.Entity
{
    public class DictDetailQueryCriteria : Pageable
    {
        public string Label { get; set; }

        public string DictName { get; set; }

        public new string[] Sort { get; set; } = new[] {"dictSort"};
    }
}
