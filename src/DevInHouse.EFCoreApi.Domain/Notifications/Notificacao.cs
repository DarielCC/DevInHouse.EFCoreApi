using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Domain.Notifications
{
    public class Notificacao
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}
