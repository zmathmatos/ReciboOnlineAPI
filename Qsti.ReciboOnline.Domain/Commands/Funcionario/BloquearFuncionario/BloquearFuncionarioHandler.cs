using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.BloquearFuncionario
{
    public class BloquearFuncionarioHandler : Notifiable, IRequestHandler<BloquearFuncionarioRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public BloquearFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario, IRepositoryUsuario repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<Response> Handle(BloquearFuncionarioRequest request, CancellationToken cancellationToken)
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

            funcionario.Bloquear(usuario, request.Motivo);

            if (IsInvalid())
            {
                return new Response(this);
            }

            _repositoryFuncionario.Editar(funcionario);

            var result = new { Id = funcionario.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            BloquearFuncionarioNotification bloquearFuncionarioNotification = new BloquearFuncionarioNotification(funcionario.Id, funcionario.Nome);
            await _mediator.Publish(bloquearFuncionarioNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
