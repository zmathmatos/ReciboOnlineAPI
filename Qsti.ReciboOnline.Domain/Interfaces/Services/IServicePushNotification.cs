using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Interfaces.Services
{
    public interface IServicePushNotification
    {
        bool Enviar(string token, string title, string body, string click_action);
    }
}
