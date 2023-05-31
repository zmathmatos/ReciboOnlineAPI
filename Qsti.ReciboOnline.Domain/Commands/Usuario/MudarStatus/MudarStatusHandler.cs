using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;


namespace Qsti.ReciboOnline.Domain.Commands.Usuario.MudarStatus
{
    public class MudarStatusHandler : Notifiable, IRequestHandler<MudarStatusRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public MudarStatusHandler(IMediator mediator, IRepositoryUsuario repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<Response> Handle(MudarStatusRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Usuario usuarioMudancaStatus = _repositoryUsuario.ObterPorId(request.ObterUsuarioBloqueio());

            if (usuarioMudancaStatus == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("UsuarioBloqueio"));
                return new Response(this);
            }

            Entities.Usuario usuario = _repositoryUsuario.ObterPorId(request.IdUsuario);

            if (usuario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"));
                return new Response(this);
            }

            usuario.MudarStatus(usuarioMudancaStatus, request.Status);


            if (IsInvalid())
            {
                return new Response(this);
            }

            _repositoryUsuario.Editar(usuario);

            var result = new { Id = usuario.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            //MudarStatusNotification MudarStatusNotification = new MudarStatusNotification(funcionario.Id, funcionario.Nome);
            //await _mediator.Publish(MudarStatusNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
