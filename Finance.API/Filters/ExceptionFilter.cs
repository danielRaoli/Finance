using Finance.API.Application.Responses;
using Finance.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;


namespace Finance.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           if(context.Exception is TransactionsException)
            {
                var transactionException = (TransactionsException)context.Exception;
                var statusCode = transactionException.GetStatusCode();
                context.HttpContext.Response.StatusCode = (int)statusCode;
                var errorJson = new JsonErrorResponse { Errors = transactionException.GetErrorMessages() };

                context.Result = new ObjectResult(new Response<JsonErrorResponse>(errorJson,(int)statusCode));  

            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errorJson = new JsonErrorResponse { Errors = ["Internal server error"] };

                context.Result = new ObjectResult(new Response<JsonErrorResponse>(errorJson, (int)HttpStatusCode.InternalServerError)); 
            }
        }
    }
}
