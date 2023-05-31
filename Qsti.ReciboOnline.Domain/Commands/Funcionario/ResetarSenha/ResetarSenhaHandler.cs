using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ResetarSenha
{
    public class ResetarSenhaHandler : Notifiable, IRequestHandler<ResetarSenhaRequest, Response>
    {
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IServiceEmail _serviceEmail;

        public ResetarSenhaHandler(IRepositoryFuncionario repositoryFuncionario, IServiceEmail serviceEmail)
        {
            _repositoryFuncionario = repositoryFuncionario;
            _serviceEmail = serviceEmail;
        }

        public async Task<Response> Handle(ResetarSenhaRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));

                return new Response(this);
            }

            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPor(x => x.Email == request.Email);

            if (funcionario == null)
            {
                AddNotification("Request", MSG.DADOS_NAO_ENCONTRADOS);
                return new Response(this);
            }

            string corpoEmail = $@"Informe seu Token -> <b>{funcionario.Id}<b> <- para poder cadastrar sua nova senha!";

            bool emailEnviado = _serviceEmail.Enviar(request.Email, "Qsti.Suporte - Alteração de Senha", corpoEmail);

            //AddNotifications(_serviceEmail.Notifications);

            if (emailEnviado == false)
            {
                AddNotification("Request", "Não conseguimos enviar e-mail com o procedimento para alterar sua senha.Entre em contato com o Administrador do sistema.");
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Criar meu objeto de resposta
            var response = new Response(this, new { Message = "Enviamos um e-mail com o procedimento para alterar sua senha!" });

            return await Task.FromResult(response);
        }
    }
}
