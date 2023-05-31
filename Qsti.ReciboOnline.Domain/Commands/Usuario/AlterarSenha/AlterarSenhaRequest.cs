using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AlterarSenha
{
    public class AlterarSenhaRequest : IRequest<Response>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
