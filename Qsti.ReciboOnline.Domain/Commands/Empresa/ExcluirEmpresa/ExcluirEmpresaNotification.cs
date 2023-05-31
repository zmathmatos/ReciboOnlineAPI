using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.ExcluirEmpresa
{
    public class ExcluirEmpresaNotification : INotification
    {
        public ExcluirEmpresaNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
