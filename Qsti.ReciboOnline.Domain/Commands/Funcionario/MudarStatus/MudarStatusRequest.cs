using MediatR;
using Qsti.ReciboOnline.Domain.Enums.Funcionario;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.MudarStatus
{
    public class MudarStatusRequest : IRequest<Response>
    {
        private Guid _idFuncionarioBloqueio;
        public MudarStatusRequest()
        {

        }
        public Guid IdFuncionario { get; set; }
        public EnumStatus Status { get; set; }

        public void SetarFuncionarioBloqueio(Guid idFuncionario)
        {
            _idFuncionarioBloqueio = idFuncionario;
        }

        public Guid ObterFuncionarioBloqueio()
        {
            return _idFuncionarioBloqueio;
        }
    }
}
