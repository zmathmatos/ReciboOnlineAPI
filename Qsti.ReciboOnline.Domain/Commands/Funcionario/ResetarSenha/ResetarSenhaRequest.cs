using MediatR;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ResetarSenha
{
    public class ResetarSenhaRequest : IRequest<Response>
    {
        public string Email { get; set; }
    }
}