using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleException(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleException(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (DomainException ex)
            {
                await HandleException(context, HttpStatusCode.UnprocessableEntity, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleException(context, HttpStatusCode.InternalServerError, "Erro interno no servidor");
            }
        }

        private static async Task HandleException(HttpContext context, HttpStatusCode status, string message)
        {
            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Status = false,
                Error = status.ToString(),
                Mensagem = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
