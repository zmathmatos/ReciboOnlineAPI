using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioRequest : IRequest<Response>
    {

        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
