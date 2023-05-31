using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Resources;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ListarRecibo
{
    public class ListarReciboHandler : Notifiable, IRequestHandler<ListarReciboRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryRecibo _repositoryRecibo;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IRepositorySolicitacao _repositorySolicitacao;

        public ListarReciboHandler(IMediator mediator, IRepositoryRecibo repositoryRecibo, IRepositoryFuncionario repositoryFuncionario, IRepositorySolicitacao repositorySolicitacao)
        {
            _mediator = mediator;
            _repositoryRecibo = repositoryRecibo;
            _repositoryFuncionario = repositoryFuncionario;
            _repositorySolicitacao = repositorySolicitacao;
        }

        public async Task<Response> Handle(ListarReciboRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var funcionario = _repositoryFuncionario.ObterPorId(request.ObterFuncionario());

            var reciboCollection = _repositorySolicitacao.Listar("Funcionario", "Matricula", "Ano", "Mes", "TipoFolha");

            reciboCollection = reciboCollection.OrderBy(x => x.Mes);



            //Devolve dados de forma customizada
            var result = reciboCollection.Select(x => new {Solicitacao = x.Id, Matricula = x.Matricula, Ano = x.Ano, Mes = x.Mes, TipoFolha = x.TipoFolha }).ToList();

            //Cria objeto de resposta
            var response = new Response(this, result);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
