using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.DesbloquearFuncionario
{
    public class DesbloquearFuncionarioNotification : INotification
    {
        public DesbloquearFuncionarioNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
