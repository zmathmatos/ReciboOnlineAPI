using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ExcluirFuncionario
{
    public class ExcluirFuncionarioNotification : INotification
    {
        public ExcluirFuncionarioNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
