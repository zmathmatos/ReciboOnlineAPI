using MediatR;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.AlterarStatusDaSolicitacao
{
    public class AlterarStatusDaSolicitacaoRequest : IRequest<Response>
    {
        public AlterarStatusDaSolicitacaoRequest(Guid idSolicitacao, EnumStatus status)
        {
            IdSolicitacao = idSolicitacao;
            Status = status;
        }

        public Guid IdSolicitacao { get; set; }
        public EnumStatus Status { get; set; }
    }
}
