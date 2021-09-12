using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XjjXmm.Door.model
{
    public class LoginModel
    {
        public string Account {get;set; }
        public string Password { get; set; }
        public string Client { get; set; }
    }

    public class AccessTokenModel
    {
        public string authorizationCode { get; set; }
        public string secret { get; set; }
    }
}
