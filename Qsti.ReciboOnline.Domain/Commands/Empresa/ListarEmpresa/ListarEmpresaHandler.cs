using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.ListarEmpresa
{
    public class ListarEmpresaHandler : Notifiable, IRequestHandler<ListarEmpresaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ListarEmpresaHandler(IMediator mediator, IRepositoryEmpresa repositoryEmpresa)
        {
            _mediator = mediator;
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<Response> Handle(ListarEmpresaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var empresaCollection = _repositoryEmpresa.Listar().OrderBy(x => x.Nome).ToList();

            //Cria objeto de resposta
            var response = new Response(this, empresaCollection);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
