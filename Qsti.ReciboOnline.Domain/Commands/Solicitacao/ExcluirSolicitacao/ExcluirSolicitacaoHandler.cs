using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.ExcluirSolicitacao
{
    public class ExcluirSolicitacaoHandler : Notifiable, IRequestHandler<ExcluirSolicitacaoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySolicitacao _repositorySolicitacao;
        private readonly IRepositoryRecibo _repositoryRecibo;

        public ExcluirSolicitacaoHandler(IMediator mediator, IRepositorySolicitacao repositorySolicitacao, IRepositoryRecibo repositoryRecibo)
        {
            _mediator = mediator;
            _repositorySolicitacao = repositorySolicitacao;
            _repositoryRecibo = repositoryRecibo;
        }

        public async Task<Response> Handle(ExcluirSolicitacaoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Solicitacao solicitacao = _repositorySolicitacao.ObterPorId(request.Id);

            if (solicitacao == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Solicitacao"));
                return new Response(this);
            }


            //Remover a solicitação
            _repositorySolicitacao.Remover(solicitacao);

            var result = new { Id = solicitacao.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            //ExcluirClienteNotification excluirClienteNotification = new ExcluirClienteNotification(solicitacao.Id, solicitacao.Nome);
            //await _mediator.Publish(excluirClienteNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}