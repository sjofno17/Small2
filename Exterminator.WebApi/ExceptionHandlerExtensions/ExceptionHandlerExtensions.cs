using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Services.Interfaces;

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            // TODO: Implement
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(exceptionHandlerFeature != null) {
                        var exception = exceptionHandlerFeature.Error;
                        var statusCode = (int) HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        ExceptionModel exceptionModel = new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message
                        };

                        var logService = app.ApplicationServices.GetService(typeof(ILogService)) as ILogService;
                        logService.LogToDatabase(exceptionModel);

                        if(exception is RecorceNotFoundException)
                        {
                            statusCode = (int) HttpStatusCode.NotFound;
                        }
                        else if(exception is ModelFormatException) 
                        {
                            statusCode = (int) HttpStatusCode.PreconditionFailed;
                        }
                        else if(exception is ArgumentOutOfRangeException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }

                        await context.Response.WriteAsync(new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message
                        }.ToString());
                    }
                });
            });

            /*
            (40%) A global exception handling should be setup within the
            UseGlobalExceptionHandler method which resides within the file
            ExceptionHandlerExtensions. The global exception handler should do the following: 
                b. Set a default return status code of Internal Server Error (500)
                e. The exception should be logged using the ILogService.LogToDatabase() method
                which accepts an ExceptionModel as parameter. The ExceptionModel should be
                properly filled out using information from the exception
                f. The response should be written out with the exception model in string format (JSON)
            */
        }
    }
}