using MediatR;
using Qsti.ReciboOnline.Domain.Enums.Usuario;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.MudarStatus
{
    public class MudarStatusRequest : IRequest<Response>
    {
        private Guid _idUsuarioBloqueio;
        public MudarStatusRequest()
        {

        }
        public Guid IdUsuario { get; set; }
        public EnumStatus Status { get; set; }

        public void SetarUsuarioBloqueio(Guid idUsuario)
        {
            _idUsuarioBloqueio = idUsuario;
        }

        public Guid ObterUsuarioBloqueio()
        {
            return _idUsuarioBloqueio;
        }
    }
}
