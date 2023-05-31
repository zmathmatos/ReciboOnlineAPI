using MediatR;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using System;
using System.Collections.Generic;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.ListarSolicitacao
{
    public class ListarSolicitacaoRequest : IRequest<IEnumerable<ListarSolicitacaoResponse>>
    {
        private Guid _idUsuario;

        public ListarSolicitacaoRequest(EnumStatus status)
        {
            Status = status;
        }

        public Guid IdUsuario { get; set; }
        public EnumStatus Status { get; set; }

        public void SetUsuario(Guid idUsuario)
        {
            _idUsuario = idUsuario;
        }
        public Guid GetUsuario()
        {
            return _idUsuario;
        }
    }
}
