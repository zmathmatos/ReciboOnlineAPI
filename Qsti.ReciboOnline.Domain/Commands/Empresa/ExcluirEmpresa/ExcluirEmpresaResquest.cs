using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.ExcluirEmpresa
{
    public class ExcluirEmpresaResquest : IRequest<Response>
    {
        public ExcluirEmpresaResquest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
