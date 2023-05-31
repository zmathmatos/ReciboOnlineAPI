using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.ExcluirEmpresa
{
    public class ExcluirEmpresaHandler : Notifiable, IRequestHandler<ExcluirEmpresaResquest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ExcluirEmpresaHandler(IMediator mediator, IRepositoryEmpresa repositoryEmpresa)
        {
            _mediator = mediator;
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<Response> Handle(ExcluirEmpresaResquest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Empresa empresa = _repositoryEmpresa.ObterPorId(request.Id);

            if (empresa == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Empresa"));
                return new Response(this);
            }

            _repositoryEmpresa.Remover(empresa);

            var result = new { Id = empresa.Id };

            //Cria objeto de resposta
            var response = new Response(this, result);

            //Cria e Dispara notificação
            ExcluirEmpresaNotification excluirEmpresaNotification = new ExcluirEmpresaNotification(empresa.Id, empresa.Nome);
            await _mediator.Publish(excluirEmpresaNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
