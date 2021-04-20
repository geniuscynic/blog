using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XjjXmm.Core.FrameWork.Common
{
    public class ErrorCodeFactory
    {
        private Dictionary<int, ErrCode> errCodes = new Dictionary<int, ErrCode>()
        {
            { 0x01, new ErrCode(0x01, "字符串不能为空", "空字符串异常")},
            { 0x02, new ErrCode(0x02, "key不存在", "key不存在异常")}
        };
        public ErrCode GetErrorCode(int code)
        {
            if (!errCodes.ContainsKey(code))
            {
                 throw new  BussinessException();
            }
            return errCodes[code];
        }
    }

    public class ErrCode
    {
        public int Code { get;  }
        public string Name { get;  }
        public string Message { get;  }

        public ErrCode(int code, string name, string message)
        {
            this.Code = code;
            this.Name = name;
            this.Message = message;
        }
    }

    public class BussinessException : Exception
    {
        public ErrCode Code { get; }

        public BussinessException(int errCode, Exception innerException = null) : base(errCode.Message, innerException)
        {
            Code = errCode;
        }
    }
}
