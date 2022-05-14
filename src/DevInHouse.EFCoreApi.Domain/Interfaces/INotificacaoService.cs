using DevInHouse.EFCoreApi.Domain.Notifications;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
