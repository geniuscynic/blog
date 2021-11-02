using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XjjXmm.Authorize.Service.Model
{
    public class JwtUserDto
    {
        public List<int> DataScopes { get; set; }    = new List<int>();

        public IEnumerable<string> Roles => User.Roles.Select(t => t.Name);

        public UserDto User { get; set; }

    }
}
