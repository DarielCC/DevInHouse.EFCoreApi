using DevInHouse.EFCoreApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Domain.Notifications
{
    public class NotificacaoService : INotificacaoService
    {
        public IList<Notificacao> Notificacoes { get; }

        public NotificacaoService()
        {
            Notificacoes = new List<Notificacao>();
        }

        public void InserirNotificacao(Notificacao notificacao) => Notificacoes.Add(notificacao);

        public bool ExistemNotificacoes() => Notificacoes.Any();

        public IEnumerable<Notificacao> ObterNotificacoes() => Notificacoes;
    }
}
