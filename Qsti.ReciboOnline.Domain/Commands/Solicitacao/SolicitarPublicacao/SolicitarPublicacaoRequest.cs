using MediatR;
using Qsti.ReciboOnline.Domain.Enums.Funcionario;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao
{
    public class SolicitarPublicacaoRequest : IRequest<Response>
    {
        private Guid _idUsuario;

        public void SetUsuario(Guid idUsuario)
        {
            _idUsuario = idUsuario;
        }
        public Guid GetUsuario()
        {
            return _idUsuario;
        }

        public Guid Id { get; set; }
        public string Ano { get; set; }
        public string Mes { get; set; }
        public string Matricula { get; set; }
        public int TipoFolha { get; set; }
        public EnumStatus Status { get; set; }

    }
}
