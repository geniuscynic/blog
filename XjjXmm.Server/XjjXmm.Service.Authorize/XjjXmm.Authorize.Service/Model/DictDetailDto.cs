using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Model
{
    public class DictDetailDto
    {
        public long Id { get; set; }

        public DictSmallDto Dict { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public int DictSort { get; set; }
    }
}
