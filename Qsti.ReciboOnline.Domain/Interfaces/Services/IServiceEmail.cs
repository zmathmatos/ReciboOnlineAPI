using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Interfaces.Services
{
    public interface IServiceEmail
    {
        bool EnviarParaUsuario(string assunto, string corpoMensagem);
        bool Enviar(string destinatario, string assunto, string corpoMensagem);
    }
}
