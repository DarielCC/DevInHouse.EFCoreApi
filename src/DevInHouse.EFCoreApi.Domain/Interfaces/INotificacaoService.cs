using DevInHouse.EFCoreApi.Domain.Notifications;
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

        bool ExistemNotificacoes();

        IEnumerable<Notificacao> ObterNotificacoes();
    }
}
