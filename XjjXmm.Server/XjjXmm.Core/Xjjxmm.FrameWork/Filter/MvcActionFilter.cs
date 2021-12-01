using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.DataValidation;
using XjjXmm.FrameWork.LogExtension;

namespace XjjXmm.FrameWork.Filter
{

    public class MvcActionFilter : IAsyncActionFilter
    {
        //  private readonly IWebHostEnvironment _env;
        private readonly ILog<MvcActionFilter> _loggerHelper;

        public MvcActionFilter(ILog<MvcActionFilter> loggerHelper)
        {
            //_env = env;
            _loggerHelper = loggerHelper;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var actionArgumentsValue in context.ActionArguments.Values)
            {
                var validator = new XjjxmmValidator(actionArgumentsValue);
                if (!validator.Validate(validateType:ValidateType.AutoValdate))
                {
                    throw BussinessException.CreateException(ExceptionCode.CustomException,
                        validator.FirstValidationResult.ErrorMessage);

                }
            }
            // throw new NotImplementedException();

           var result =  await next();
           OnActionExecuted(result);

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                 _loggerHelper.Error("MVC Error", context.Exception);

                //if (context.Exception is BussinessException bussinessException)
                //{
                //    context.Result = new JsonResult(new BussinessModel<string>(bussinessException.ExceptionModel.Name)
                //    {
                //        Success = false,
                //        Status = (int)bussinessException.ExceptionModel.Code,
                //        Message = bussinessException.ExceptionModel.Message
                //    });

                //    //context.Exception = null;
                //}
            }
            else if (context.Result is FileResult)
            {
                
            }
            else
            {
                var result = (ObjectResult)context.Result;
                context.Result = new JsonResult(new BussinessModel<object>(result.Value));
            }

            //throw new NotImplementedException();
        }


    }
}
