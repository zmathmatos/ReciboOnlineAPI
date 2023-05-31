using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario
{
    public class AutenticarFuncionarioRequest : IRequest<AutenticarFuncionarioResponse>
    {
        public AutenticarFuncionarioRequest()
        {

        }
        public AutenticarFuncionarioRequest(string matricula, string senha)
        {
            Matricula = matricula;
            Senha = senha;
        }

        public AutenticarFuncionarioRequest(string matricula, string senha, string tokenPush)
        {
            Matricula = matricula;
            Senha = senha;
            TokenPush = tokenPush;
        }

        public string Matricula { get; set; }
        public string Senha { get; set; }
        public string TokenPush { get; set; }
    }
}
