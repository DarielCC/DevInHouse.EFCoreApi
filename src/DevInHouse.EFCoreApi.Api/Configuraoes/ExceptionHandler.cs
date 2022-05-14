using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevInHouse.EFCoreApi.Api.Configuraoes
{
    public static class ExceptionHandler
    {
        public static void UseOnExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder => builder.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature is null)
                    return;

                var exception = exceptionHandlerFeature.Error;

                var problemDetails = new ProblemDetails()
                {
                    Title = "Erro do sistema",
                    Detail = env.IsDevelopment() ? exception.Message : "Erro inesperado do sistema",
                    Status = StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.ContentType = "application/problem+json";

                var json = JsonConvert.SerializeObject(problemDetails);

                await context.Response.WriteAsync(json);
            }));
        }
    }
}
