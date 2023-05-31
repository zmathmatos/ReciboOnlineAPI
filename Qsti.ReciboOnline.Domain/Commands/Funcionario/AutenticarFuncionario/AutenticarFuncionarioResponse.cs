using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario
{
    public class AutenticarFuncionarioResponse
    {
        public string Matricula { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Autenticado { get; set; }
        public string Mensagem { get; set; }


        public static explicit operator AutenticarFuncionarioResponse(Entities.Funcionario funcionario)
        {
            return new AutenticarFuncionarioResponse()
            {
                Matricula = funcionario.Matricula,
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Autenticado = true
            };
        }
    }
}
