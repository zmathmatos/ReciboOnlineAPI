using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.BloquearFuncionario;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.DesbloquearFuncionario
{
    public class DesbloquearFuncionarioHandler : Notifiable, IRequestHandler<DesbloquearFuncionarioRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public DesbloquearFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario, IRepositoryUsuario repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<Response> Handle(DesbloquearFuncionarioRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Usuario usuario = _repositoryUsuario.ObterPorId(request.IdUsuario.Value);

            if (usuario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuario"));
                return new Response(this);
            }

            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPorId(request.IdFuncionario);

            if (funcionario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Funcionario"));
                return new Response(this);
            }

            funcionario.Desbloquear(usuario, request.Motivo);

            if (IsInvalid())
            {
                return new Response(this);
            }

            _repositoryFuncionario.Editar(funcionario);

            var result = new { Id = funcionario.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            DesbloquearFuncionarioNotification desbloquearFuncionarioNotification = new DesbloquearFuncionarioNotification(funcionario.Id, funcionario.Nome);
            await _mediator.Publish(desbloquearFuncionarioNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
