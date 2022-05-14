using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace DevInHouse.EFCoreApi.Api.Configuraoes
{
    public class NotificacaoFilter : IAsyncResultFilter
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoFilter(INotificacaoService notificacaoService) => _notificacaoService = notificacaoService;   

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!_notificacaoService.ExistemNotificacoes())
            {
                next();
                return;
            }

            var notificacoes = _notificacaoService.ObterNotificacoes();

            var problemDetails = notificacoes.Count() == 1
                ? CriarNotificacao(notificacoes.FirstOrDefault())
                : CriarNotificacoes(notificacoes);

            context.HttpContext.Response.StatusCode = problemDetails.Status.Value;
            context.HttpContext.Response.ContentType = "application/problem+json";

            var json = JsonConvert.SerializeObject(problemDetails);

            await context.HttpContext.Response.WriteAsync(json);
        }

        private ProblemDetails CriarNotificacao(Notificacao notificacao) => new ProblemDetails
        {
            Title = "Erro em regra de negocios",
            Detail = notificacao.Mensagem,
            Status = (int)notificacao.StatusCode
        };

        private ProblemDetails CriarNotificacoes(IEnumerable<Notificacao> notificacoes)
        {
            var problemDetail = new ValidationProblemDetails
            {
                Title = "Erro em regra de negocios",
                Detail = "Por favor verificar a lista de erros",
                Status = (int)HttpStatusCode.UnprocessableEntity
            };

            foreach (var notification in notificacoes)
                problemDetail.Errors.Add(new KeyValuePair<string, string[]>(notification.Mensagem, new string[] { notification.Mensagem }));

            return problemDetail;
        }
    }
}
