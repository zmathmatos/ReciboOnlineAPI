using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qsti.ReciboOnline.Domain.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario
{
    public class AutenticarUsuarioHandler : Notifiable, IRequestHandler<AutenticarUsuarioRequest, AutenticarUsuarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryPushNotification _repositoryPushNotification;

        public AutenticarUsuarioHandler(IMediator mediator, IRepositoryUsuario repositoryUsuario, IRepositoryPushNotification repositoryPushNotification)
        {
            _mediator = mediator;
            _repositoryUsuario = repositoryUsuario;
            _repositoryPushNotification = repositoryPushNotification;
        }

        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", "Request é obrigatório");
                return null;
            }

            request.Senha = request.Senha.ConvertToMD5();

            Entities.Usuario usuario = _repositoryUsuario.ObterPor(x => x.Email == request.Email && x.Senha == request.Senha);

            if (usuario == null)
            {
                AddNotification("Usuario", "Usuário não encontrado.");
                return new AutenticarUsuarioResponse()
                {
                    Mensagem = "Usuário não encontrado.",
                    Autenticado = false
                };
            }

            if (usuario.Status == Enums.Usuario.EnumStatus.Inativo)
            {
                AddNotification("Request", "Usuário não está ativo no sistema.");

                return new AutenticarUsuarioResponse()
                {
                    Mensagem = "Usuário não está ativo, comunique algum administrador do sistema e solicite sua ativação!",
                    Autenticado = false
                };
            }

            if (usuario.Status == Enums.Usuario.EnumStatus.Bloqueado)
            {
                AddNotification("Request", "Usuário bloquado no sistema.");

                return new AutenticarUsuarioResponse()
                {
                    Mensagem = "Usuário bloquado, comunique algum administrador do sistema e solicite sua ativação!",
                    Autenticado = false
                };
            }

            if (!string.IsNullOrEmpty(request.TokenPush))
            {
                if (_repositoryPushNotification.Existe(x => x.Token == request.TokenPush && x.Usuario != null && x.Usuario.Id == usuario.Id, x => x.Usuario) == false)
                {
                    PushNotification push = new PushNotification(usuario, request.TokenPush);

                    if (IsValid())
                    {
                        _repositoryPushNotification.Adicionar(push);
                    }
                }
            }

            //Cria objeto de resposta
            var response = (AutenticarUsuarioResponse)usuario;

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
