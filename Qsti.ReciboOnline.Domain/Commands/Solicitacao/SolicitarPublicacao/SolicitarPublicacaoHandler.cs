using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao
{
    public class SolicitarPublicacaoHandler : Notifiable, IRequestHandler<SolicitarPublicacaoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositorySolicitacao _repositorySolicitacao;
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryRecibo _repositoryRecibo;

        public SolicitarPublicacaoHandler(IMediator mediator, IRepositorySolicitacao repositorySolicitacao, IRepositoryUsuario repositoryUsuario, IRepositoryRecibo repositoryRecibo)
        {
            _mediator = mediator;
            _repositorySolicitacao = repositorySolicitacao;
            _repositoryUsuario = repositoryUsuario;
            _repositoryRecibo = repositoryRecibo;

        }

        public async Task<Response> Handle(SolicitarPublicacaoRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Usuario usuario = _repositoryUsuario.ObterPorId(request.GetUsuario());
            if (usuario == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuário"));
            }


            Entities.Solicitacao solicitacao = new Entities.Solicitacao(usuario, request.Matricula, request.TipoFolha, request.Ano, request.Mes) ;

            AddNotifications(solicitacao);

            if (IsInvalid())
            {
                return new Response(this);
            }

            solicitacao = _repositorySolicitacao.Adicionar(solicitacao);

            //Criar meu objeto de resposta
            var response = new Response(this, solicitacao);


            return await Task.FromResult(response);
        }
    }
}
