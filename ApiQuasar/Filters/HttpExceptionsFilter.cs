using ApiQuasar.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Filters
{
    public class HttpExceptionsFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = new HttpException("Internal Server Error" , HttpStatusCode.InternalServerError);
            context.HttpContext.Response.StatusCode = 500;

            if (context.Exception is Exception)
            {
                error = context.Exception as HttpException;
                context.HttpContext.Response.StatusCode = (int)error.ErrorCode;
            }

            context.Result = new JsonResult(new { error = error.MessageError });
            base.OnException(context);
        }

    }
}
