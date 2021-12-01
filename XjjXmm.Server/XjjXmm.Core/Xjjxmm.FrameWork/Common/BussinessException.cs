using System;
using System.Collections.Generic;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.FrameWork.Common
{


    //public enum ExceptionCode
    //{
    //    EmptyOrNullString = 1,
    //    KeyNotExist = 2,
    //    CustomException = 99
    //}

    public class ExceptionModel
    {
        public long Code { get; }
        public string CodeDesc { get; set; }
        public string Message { get; set; }

        //public ExceptionModel(string code, string message)
        //{
        //    Code = code;
        //    // Name = name;
        //    Message = message;

        //}

        public ExceptionModel(long code, string codeDesc)
        {
            Code = code;
            // Name = name;
            this.CodeDesc = codeDesc;

            this.Message = codeDesc;

        }

        
        //public static ExceptionModel ValidationError  = new ExceptionModel("X0001", "验证失败");
        //public static ExceptionModel GloblError = new ExceptionModel("X9999", "未捕获的异常");
    }

    public class BussinessException : Exception
    {
        //public string Code { get; set; }
        //public string Message { get; set; }

        //private BussinessException(string code, string message, Exception innerException = null) : base(message, innerException)
        //{
        //    this.Code = code;
        //    this.Message = message;
        //}
        public ExceptionModel ExceptionModel { get; set; }

        public BussinessException(ExceptionModel exceptionModel, Exception innerException = null) : base(exceptionModel.Message, innerException)
        {
            this.ExceptionModel = exceptionModel;
        }

        public BussinessException(ExceptionModel exceptionModel, string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
            this.ExceptionModel = exceptionModel;
            this.ExceptionModel.Message = errorMessage;
        }

        public BussinessException(StatusCodes status, string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
            this.ExceptionModel = new ExceptionModel(status.ToInt64(), status.ToDescription())
            {
                Message = errorMessage
            };
        }

        //private static Dictionary<ExceptionCode, ExceptionModel> errCodes = new Dictionary<ExceptionCode, ExceptionModel>()
        //{
        //    { ExceptionCode.EmptyOrNullString, new ExceptionModel(ExceptionCode.EmptyOrNullString, "字符串不能为空", "空字符串异常")},
        //    { ExceptionCode.KeyNotExist, new ExceptionModel(ExceptionCode.KeyNotExist, "key不存在", "key不存在异常")},
        //    { ExceptionCode.CustomException, new ExceptionModel(ExceptionCode.CustomException, "自定义异常", "")},
        //};

        //public static BussinessException CreateException(ExceptionCode code, Exception innerException)
        //{
        //    return CreateException(code, "", innerException);
        //}

        //public static BussinessException CreateException(string code, string errorMessage,
        // Exception innerException = null)
        //{
        //if (!errCodes.ContainsKey(code))
        //{
        //    throw BussinessException.CreateException(ExceptionCode.KeyNotExist);
        //}

        //var errorCode = errCodes[code];
        // if (!string.IsNullOrWhiteSpace(errorMessage))
        // {
        // errorCode.Message = errorMessage;
        //}

        //return new BussinessException(new ExceptionModel(code,errorMessage), innerException);

        //}
    }
}
