using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.ExcluirSolicitacao
{
    public class ExcluirSolicitacaoRequest : IRequest<Response>
    {
        public ExcluirSolicitacaoRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
