
using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AdicionarUsuario.Notifications
{
    public class AvisarAdministradores : INotificationHandler<AdicionarUsuarioNotification>
    {
        public async Task Handle(AdicionarUsuarioNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Enviar email de ativação para o usuário " + notification.Usuario.Nome);
        }
    }
}