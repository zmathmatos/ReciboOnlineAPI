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


namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.MudarStatus
{
    public class MudarStatusHandler : Notifiable, IRequestHandler<MudarStatusRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;

        public MudarStatusHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
        }

        public async Task<Response> Handle(MudarStatusRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Funcionario funcionarioMudancaStatus = _repositoryFuncionario.ObterPorId(request.ObterFuncionarioBloqueio());

            if (funcionarioMudancaStatus == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("FuncionarioBloqueio"));
                return new Response(this);
            }

            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPorId(request.IdFuncionario);

            if (funcionario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Funcionario"));
                return new Response(this);
            }

            funcionario.MudarStatus(funcionarioMudancaStatus, request.Status);


            if (IsInvalid())
            {
                return new Response(this);
            }

            _repositoryFuncionario.Editar(funcionario);

            var result = new { Id = funcionario.Id };

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
