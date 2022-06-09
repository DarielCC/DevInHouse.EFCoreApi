using DevInHouse.EFCoreApi.Domain.Notifications;
using FluentValidation.Results;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface INotificacaoService
    {
        void InserirNotificacao(Notificacao notificacao);
        void InserirNotificacoes(ValidationResult validationResult);

        bool ExistemNotificacoes();

        IEnumerable<Notificacao> ObterNotificacoes();
    }
}
