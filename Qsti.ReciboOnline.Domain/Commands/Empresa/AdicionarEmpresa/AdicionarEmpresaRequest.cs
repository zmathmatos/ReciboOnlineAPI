using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Empresa.AdicionarEmpresa
{
    public class AdicionarEmpresaRequest : IRequest<Response>
    {
        public string Nome { get; set; }
    }
}
