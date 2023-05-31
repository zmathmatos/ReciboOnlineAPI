using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.ExcluirFuncionario;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.RemoverFuncionario
{
    public class ExcluirFuncionarioHandler : Notifiable, IRequestHandler<ExcluirFuncionarioResquest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;

        public ExcluirFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
        }

        public async Task<Response> Handle(ExcluirFuncionarioResquest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Funcionario Funcionario = _repositoryFuncionario.ObterPorId(request.Id);

            if (Funcionario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Funcionario"));
                return new Response(this);
            }

            _repositoryFuncionario.Remover(Funcionario);

            var result = new { Id = Funcionario.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            ExcluirFuncionarioNotification excluirFuncionarioNotification = new ExcluirFuncionarioNotification(Funcionario.Id, Funcionario.Nome);
            await _mediator.Publish(excluirFuncionarioNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
