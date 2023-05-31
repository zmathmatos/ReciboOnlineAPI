using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Linq;

namespace Qsti.ReciboOnline.Domain.Commands
{
    public class Response
    {
        protected Response()
        {
            Notifications = new List<Notification>();
        }

        public Response(INotifiable notifiable)
        {
            Success = notifiable.IsValid();
            Notifications = notifiable.Notifications;
        }

        public Response(INotifiable notifiable, object data)
            : this(notifiable)
        {
            Data = data;
        }

        public IEnumerable<Notification> Notifications { get; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
       
    }
}
