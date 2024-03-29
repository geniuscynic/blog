﻿using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using DoCare.Zkzx.Core.FrameWork.Tool.DataValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;


namespace XjjXmm.Core.FrameWork.Filter
{

    public class MvcActionFilter : IAsyncActionFilter
    {
        //  private readonly IWebHostEnvironment _env;
        private readonly ILogger _loggerHelper;

        public MvcActionFilter(ILogger loggerHelper)
        {
            //_env = env;
            _loggerHelper = loggerHelper;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var actionArgumentsValue in context.ActionArguments.Values)
            {
                var validator = new DoCareValidator(actionArgumentsValue);
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
                //_loggerHelper.Error(context.Exception, "MVC Error");

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
