using System.Net;

namespace DevInHouse.EFCoreApi.Domain.Notifications
{
    public class Notificacao
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}
