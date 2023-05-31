using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.ResetarSenha
{
    public class ResetarSenhaRequest : IRequest<Response>
    {
        public string Email { get; set; }
    }
}
