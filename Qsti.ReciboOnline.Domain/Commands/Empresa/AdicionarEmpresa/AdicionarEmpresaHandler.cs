using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.AdicionarEmpresa
{
    public class AdicionarEmpresaHandler : Notifiable, IRequestHandler<AdicionarEmpresaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public AdicionarEmpresaHandler(IMediator mediator, IRepositoryEmpresa repositoryEmpresa)
        {
            _mediator = mediator;
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<Response> Handle(AdicionarEmpresaRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));

                return new Response(this);
            }

            Entities.Empresa Empresa = new Entities.Empresa(request.Nome);

            AddNotifications(Empresa);

            if (IsInvalid())
            {
                return new Response(this);
            }

            Empresa = _repositoryEmpresa.Adicionar(Empresa);

            //Criar meu objeto de resposta
            var response = new Response(this, Empresa);

            //Dispara uma notificação que um novo sistema foi cadastrado
            Empresa.AdicionarEmpresa.AdicionarEmpresaNotification adicionarEmpresaNotification = new Empresa.AdicionarEmpresa.AdicionarEmpresaNotification(Empresa.Id, Empresa.Nome);

            await _mediator.Publish(adicionarEmpresaNotification);

            return await Task.FromResult(response);
        }
    }
}
