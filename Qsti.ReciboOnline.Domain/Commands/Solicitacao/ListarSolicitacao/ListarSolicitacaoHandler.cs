using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.ListarSolicitacao
{
    public class ListarSolicitacaoHandler : Notifiable, IRequestHandler<ListarSolicitacaoRequest, IEnumerable<ListarSolicitacaoResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySolicitacao _repositorySolicitacao;

        public ListarSolicitacaoHandler(IMediator mediator, IRepositorySolicitacao repositorySolicitacao)
        {
            _mediator = mediator;
            _repositorySolicitacao = repositorySolicitacao;
        }

        public async Task<IEnumerable<ListarSolicitacaoResponse>> Handle(ListarSolicitacaoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                //return new Response(this);
                return null;
            }

            var collection = _repositorySolicitacao.ListarPor(x => x.Usuario.Id == request.GetUsuario()
                                                             && (x.Status == request.Status 
                                                                ), x => x.Usuario)
                .Select(x => new ListarSolicitacaoResponse() {Id=x.Id, Ano = x.Ano, Mes=x.Mes, Status=x.Status, Matricula = x.Matricula, TipoFolha = x.TipoFolha }).ToList<ListarSolicitacaoResponse>();


            //var response = new ListarSolicitacaoResponse(collection);

            //Cria objeto de resposta
            //var response = new Response(this, collection);

            ////Retorna o resultado
            //return await Task.FromResult(response);

            //return response;

            return collection;
        }
    }
}
