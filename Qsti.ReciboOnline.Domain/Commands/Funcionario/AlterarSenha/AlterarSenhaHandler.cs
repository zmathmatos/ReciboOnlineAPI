using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarSenha
{
    public class AlterarSenhaHandler : Notifiable, IRequestHandler<AlterarSenhaRequest, Response>
    {
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IServiceEmail _serviceEmail;

        public AlterarSenhaHandler(IRepositoryFuncionario repositoryFuncionario, IServiceEmail serviceEmail)
        {
            _repositoryFuncionario = repositoryFuncionario;
            _serviceEmail = serviceEmail;
        }

        public async Task<Response> Handle(AlterarSenhaRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));

                return new Response(this);
            }
            Guid idUsuario;
            try
            {
                idUsuario = Guid.Parse(request.Token);
            }
            catch (Exception)
            {
                AddNotification("Request", "TOKEN INVÁLIDO!");
                return new Response(this);
            }

            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPor(x => x.Email == request.Email && x.Id == idUsuario);

            if (funcionario == null)
            {
                AddNotification("Request", "E-MAIL OU TOKEN INVÁLIDO!");
                return new Response(this);
            }

            funcionario.AlterarSenha(request.NovaSenha);

            if (IsInvalid())
            {
                return new Response(this);
            }

            funcionario = _repositoryFuncionario.Editar(funcionario);

            //Criar meu objeto de resposta
            var response = new Response(this, new { Message = "Operação realizada com sucesso!" });

            return await Task.FromResult(response);
        }
    }
}
