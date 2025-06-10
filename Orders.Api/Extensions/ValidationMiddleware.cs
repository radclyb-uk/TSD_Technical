using Orders.Application.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ValidationException = Orders.Application.Common.ValidationException;

namespace Orders.Api.Extensions
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Success = false, Message = "Validation Errors", Errors = ex.Errors.ToList() });
            }
        }
    }
}
