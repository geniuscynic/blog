using System;
using System.Collections.Generic;

namespace DoCare.Zkzx.Core.FrameWork.Tool.Common
{

    public enum ExceptionCode
    {
        EmptyOrNullString = 1,
        KeyNotExist = 2,
        CustomException = 99
    }

    public class ExceptionModel
    {
        public ExceptionCode Code { get; }
        public string Name { get; }
        public string Message { get; set; }

        public ExceptionModel(ExceptionCode code, string name, string message)
        {
            Code = code;
            Name = name;
            Message = message;
        }
    }

    public class BussinessException : Exception
    {
        public ExceptionModel ExceptionModel { get; set; }

        private BussinessException(ExceptionModel exceptionModel, Exception innerException = null) : base(exceptionModel.Message, innerException)
        {
            this.ExceptionModel = exceptionModel;
        }

        private static Dictionary<ExceptionCode, ExceptionModel> errCodes = new Dictionary<ExceptionCode, ExceptionModel>()
        {
            { ExceptionCode.EmptyOrNullString, new ExceptionModel(ExceptionCode.EmptyOrNullString, "字符串不能为空", "空字符串异常")},
            { ExceptionCode.KeyNotExist, new ExceptionModel(ExceptionCode.KeyNotExist, "key不存在", "key不存在异常")},
            { ExceptionCode.CustomException, new ExceptionModel(ExceptionCode.CustomException, "自定义异常", "")},
        };

        public static BussinessException CreateException(ExceptionCode code, Exception innerException)
        {
            return CreateException(code, "", innerException);
        }

        public static BussinessException CreateException(ExceptionCode code, string errorMessage = "",
            Exception innerException = null)
        {
            if (!errCodes.ContainsKey(code))
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist);
            }

            var errorCode = errCodes[code];
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                errorCode.Message = errorMessage;
            }

            return new BussinessException(errorCode);

        }
    }
}
