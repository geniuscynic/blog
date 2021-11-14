using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Authorize.Service.Model
{
    public class DictDto
    {
        public long Id { get; set; }

        public List<DictDetailDto> DictDetails { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
