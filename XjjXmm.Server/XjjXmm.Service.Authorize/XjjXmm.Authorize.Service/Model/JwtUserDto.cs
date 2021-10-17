using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XjjXmm.Authorize.Service.Model
{
    public class JwtUserDto : UserDetailModel
    {

        public List<string> Roles { get; set; } = new List<string>();

           [JsonIgnore]
        
        public new string Password { get; set; }

        [JsonIgnore]

        public new string UserName { get; set; }

        [JsonIgnore]

        public new bool IsEnabled { get; set; }
      
    }
}
