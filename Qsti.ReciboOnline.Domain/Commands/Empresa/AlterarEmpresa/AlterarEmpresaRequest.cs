using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.AlterarEmpresa
{
    public class AlterarEmpresaRequest : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
