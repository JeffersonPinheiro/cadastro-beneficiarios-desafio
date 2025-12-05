using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;

                        switch (exception)
                        {
                            case NotFoundException:
                                context.Response.StatusCode = StatusCodes.Status404NotFound;
                                break;

                            case BadRequestException:
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                break;

                            case UnauthorizedException:
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                break;

                            default:
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }

                        var errorDetails = new Models.ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = exception.Message,
                            TraceId = context.TraceIdentifier
                        };

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }

}
