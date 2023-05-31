using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ObterFuncionarioPorId
{
    public class ObterFuncionarioPorIdHandler : Notifiable, IRequestHandler<ObterFuncionarioPorIdRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;

        public ObterFuncionarioPorIdHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
        }

        public async Task<Response> Handle(ObterFuncionarioPorIdRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Funcionario Funcionario = _repositoryFuncionario.ObterPorId(request.IdFuncionario);

            if (Funcionario == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Funcionario"));
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Cria objeto de resposta
            var response = new Response(this, Funcionario);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
