using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Util.EnviarAvisoPush
{
    public class EnviarAvisoPushRequest : IRequest<Response>
    {
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Link { get; set; }
        public bool AvisarUsuario { get; set; }
        public bool AvisarFuncionario { get; set; }
    }
}
