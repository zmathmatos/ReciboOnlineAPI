using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.AlterarStatusDaSolicitacao
{
    public class AlterarStatusDaSolicitacaoHandler : Notifiable, IRequestHandler<AlterarStatusDaSolicitacaoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySolicitacao _repositorySolicitacao;

        public AlterarStatusDaSolicitacaoHandler(IMediator mediator, IRepositorySolicitacao repositorySolicitacao)
        {
            _mediator = mediator;
            _repositorySolicitacao = repositorySolicitacao;
        }

        public async Task<Response> Handle(AlterarStatusDaSolicitacaoRequest request, CancellationToken cancellationToken)
        {
            //Validar se o request veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Solicitacao"));
                return new Response(this);
            }

            Entities.Solicitacao solicitacao = _repositorySolicitacao.ObterPorId(request.IdSolicitacao);

            if (solicitacao == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Solicitacao"));
                return new Response(this);
            }

            solicitacao.AlterarStatus(request.Status);

            AddNotifications(solicitacao);

            if (IsInvalid())
            {
                return new Response(this);
            }

            solicitacao = _repositorySolicitacao.Editar(solicitacao);

            //Criar meu objeto de resposta
            var response = new Response(this, solicitacao);
            return await Task.FromResult(response);
        }
    }
}
