using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AdicionarFuncionario
{
    public class AdicionarFuncionarioNotification : INotification
    {
        public AdicionarFuncionarioNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        
    }
}
