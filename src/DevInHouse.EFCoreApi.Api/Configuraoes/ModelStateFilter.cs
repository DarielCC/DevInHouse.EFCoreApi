using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevInHouse.EFCoreApi.Api.Configuraoes
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var modelStateKeys = context.ModelState.Select(entry => entry.Key);

            var errors = new ModelStateDictionary();

            foreach (var key in modelStateKeys)
            {
                var modelStateErrors = context.ModelState
                    .Where(entry => entry.Key == key)
                    .Select(entry => entry.Value.Errors.Select(error => error.ErrorMessage))
                    .Aggregate(Enumerable.Empty<string>(), (agg, val) => agg.Concat(val));

                foreach (var error in modelStateErrors)
                    errors.AddModelError(key, error);
            }

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "Model State Error",
                Instance = $"{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Para mais detalhes ver lista de erros"
            };

            context.Result = new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json", "application/problem+xml" }
            };
        }
    }
}
