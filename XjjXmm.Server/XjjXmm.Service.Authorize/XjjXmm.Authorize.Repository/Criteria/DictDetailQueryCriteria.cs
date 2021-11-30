using XjjXmm.FrameWork.Common;

namespace XjjXmm.Authorize.Repository.Criteria
{
    public class DictDetailQueryCriteria : Pageable
    {
        public string Label { get; set; }

        public string DictName { get; set; }

        public new string[] Sort { get; set; } = new[] {"dictSort"};


      


    }
}
