using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarFuncionario
{
    public class AlterarFuncionarioRequest : IRequest<Response>
    {
        public Guid Matricula { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
