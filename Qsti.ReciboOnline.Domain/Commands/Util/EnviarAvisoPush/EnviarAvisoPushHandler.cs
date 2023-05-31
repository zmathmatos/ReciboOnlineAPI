using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Util.EnviarAvisoPush
{
    public class EnviarAvisoPushHandler : Notifiable, IRequestHandler<EnviarAvisoPushRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryPushNotification _repositoryPushNotification;
        private readonly IServicePushNotification _servicePushNotification;

        public EnviarAvisoPushHandler(IMediator mediator, IRepositoryPushNotification repositoryPushNotification, IServicePushNotification servicePushNotification)
        {
            _mediator = mediator;
            _repositoryPushNotification = repositoryPushNotification;
            _servicePushNotification = servicePushNotification;
        }

        public async Task<Response> Handle(EnviarAvisoPushRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));

                return new Response(this);
            }

            List<Entities.PushNotification> collection = new List<Entities.PushNotification>();
            if (request.AvisarFuncionario)
            {
                collection.AddRange(_repositoryPushNotification.ListarPor(x => x.Funcionario != null, x => x.Funcionario));
            }
            if (request.AvisarUsuario)
            {
                collection.AddRange(_repositoryPushNotification.ListarPor(x => x.Usuario != null, x => x.Usuario));
            }

            collection.ToList().ForEach(push => {
                _servicePushNotification.Enviar(push.Token, request.Assunto, request.Mensagem, request.Link);
                //Thread.Sleep(TimeSpan.FromSeconds(1));
            });

            //Criar meu objeto de resposta
            var response = new Response(this, "Enviado");

            return await Task.FromResult(response);
        }
    }
}
