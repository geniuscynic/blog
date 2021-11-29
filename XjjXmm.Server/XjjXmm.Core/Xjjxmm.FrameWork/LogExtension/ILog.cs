using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace XjjXmm.FrameWork.LogExtension
{
    public interface ILog<T>
    {
        void Debug(string msg);
        void Info(string msg);
        void Trace(string msg, Exception ex);
        void Error(string msg, Exception ex);

        void Critical(string msg, Exception ex);


    }
}
