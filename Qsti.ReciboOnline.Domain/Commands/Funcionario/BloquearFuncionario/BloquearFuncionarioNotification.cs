using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.BloquearFuncionario
{
    public class BloquearFuncionarioNotification : INotification
    {
        public BloquearFuncionarioNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
