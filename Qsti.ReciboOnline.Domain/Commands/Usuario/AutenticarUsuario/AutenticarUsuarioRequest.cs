using MediatR;


namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario
{
    public class AutenticarUsuarioRequest : IRequest<AutenticarUsuarioResponse>
    {
        public AutenticarUsuarioRequest()
        {

        }
        public AutenticarUsuarioRequest(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public AutenticarUsuarioRequest(string email, string senha, string tokenPush)
        {
            Email = email;
            Senha = senha;
            TokenPush = tokenPush;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string TokenPush { get; set; }
    }
}
