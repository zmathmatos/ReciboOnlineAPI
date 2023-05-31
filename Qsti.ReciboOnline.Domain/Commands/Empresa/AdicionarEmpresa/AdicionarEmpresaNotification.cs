using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.AdicionarEmpresa
{
    public class AdicionarEmpresaNotification : INotification
    {
        public AdicionarEmpresaNotification(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        
    }
}
