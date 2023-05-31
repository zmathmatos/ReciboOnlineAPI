using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarSenha
{
    public class AlterarSenhaRequest : IRequest<Response>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
