using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.BloquearFuncionario
{
    public class BloquearFuncionarioRequest : IRequest<Response>
    {
        public Guid IdFuncionario { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Motivo { get; set; }
    }
}
