using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AdicionarFuncionario
{
    public class AdicionarFuncionarioRequest : IRequest<Response>
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get;  set; }
        public string Senha { get;  set; }
    }
}
