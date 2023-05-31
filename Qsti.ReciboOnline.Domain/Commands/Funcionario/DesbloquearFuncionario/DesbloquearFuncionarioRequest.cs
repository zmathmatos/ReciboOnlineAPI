using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.DesbloquearFuncionario
{
    public class DesbloquearFuncionarioRequest : IRequest<Response>
    {
        public Guid IdFuncionario { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Motivo { get; set; }
    }
}
