using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Usuario.AlterarSenha
{
    public class AlterarSenhaHandler : Notifiable, IRequestHandler<AlterarSenhaRequest, Response>
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        public readonly IServiceEmail _serviceEmail;

        public AlterarSenhaHandler(IRepositoryUsuario repositoryUsuario, IServiceEmail serviceEmail)
        {
            _repositoryUsuario = repositoryUsuario;
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

            Entities.Usuario usuario = _repositoryUsuario.ObterPor(x => x.Email == request.Email && x.Id==idUsuario);

            if (usuario == null)
            {
                AddNotification("Request", "E-MAIL OU TOKEN INVÁLIDO!");
                return new Response(this);
            }

            usuario.AlterarSenha(request.NovaSenha);

            if (IsInvalid())
            {
                return new Response(this);
            }

            usuario = _repositoryUsuario.Editar(usuario);

            //Criar meu objeto de resposta
            var response = new Response(this, new { Message = "Operação realizada com sucesso!" });

            return await Task.FromResult(response);
        }
    }
}
