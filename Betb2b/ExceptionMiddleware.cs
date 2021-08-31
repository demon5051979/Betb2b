using B2B.Domain.Dto;
using B2B.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Betb2b
{
	public class ExceptionMiddleware
	{
        public readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorResponse response = new ErrorResponse();
            response.ErrorMsg = ex.Message;

            if (context.Request.ContentType != null && context.Request.ContentType.ToLower().StartsWith("application/xml"))
            {
                var serializer = new XmlSerializer(typeof(ErrorResponse));
                return context.Response.WriteAsync(serializer.SerializeToString(response));
            }
            else
            {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
