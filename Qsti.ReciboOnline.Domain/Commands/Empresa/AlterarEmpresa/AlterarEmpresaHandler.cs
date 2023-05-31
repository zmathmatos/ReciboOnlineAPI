using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.AlterarEmpresa
{
    public class AlterarEmpresaHandler : Notifiable, IRequestHandler<AlterarEmpresaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public AlterarEmpresaHandler(IMediator mediator, IRepositoryEmpresa repositoryEmpresa)
        {
            _mediator = mediator;
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<Response> Handle(AlterarEmpresaRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Empresa"));
                return new Response(this);
            }


            //Entities.Empresa Empresa = _repositoryEmpresa.ObterPorId(request.Id);

            //if (Empresa == null)
            //{
            //    AddNotification("Empresa", MSG.X0_NAO_INFORMADO.ToFormat("Empresa"));
            //    return new Response(this);
            //}

            //Empresa.AlterarEmpresa(request.Nome);

            //AddNotifications(Empresa);


            //if (IsInvalid())
            //{
            //    return new Response(this);
            //}



            //Empresa = _repositoryEmpresa.Editar(Empresa);

            //Criar meu objeto de resposta
            //var response = new Response(this, Empresa);

            var response = new Response(this, "ok");

            return await Task.FromResult(response);
        }
    }
}
