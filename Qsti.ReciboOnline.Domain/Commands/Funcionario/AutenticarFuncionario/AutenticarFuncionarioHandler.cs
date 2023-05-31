using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qsti.ReciboOnline.Domain.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario;


namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario
{
    public class AutenticarFuncionarioHandler : Notifiable, IRequestHandler<AutenticarFuncionarioRequest, AutenticarFuncionarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IRepositoryPushNotification _repositoryPushNotification;

        public AutenticarFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario, IRepositoryPushNotification repositoryPushNotification)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
            _repositoryPushNotification = repositoryPushNotification;
        }

        public async Task<AutenticarFuncionarioResponse> Handle(AutenticarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", "Request é obrigatório");
                return null;
            }

            request.Senha = request.Senha.ConvertToMD5();

            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPor(x => x.Matricula == request.Matricula && x.Senha == request.Senha);

            if (funcionario == null)
            {
                AddNotification("Funcionario", "Usuário não encontrado.");
                return new AutenticarFuncionarioResponse()
                {
                    Mensagem = "Usuário não encontrado.",
                    Autenticado = false
                };
            }

            if (funcionario.Status == Enums.Funcionario.EnumStatus.Inativo)
            {
                AddNotification("Request", "Usuário não está ativo no sistema.");

                return new AutenticarFuncionarioResponse()
                {
                    Mensagem = "Usuário não está ativo, comunique algum administrador do sistema e solicite sua ativação!",
                    Autenticado = false
                };
            }

            if (funcionario.Status == Enums.Funcionario.EnumStatus.Bloqueado)
            {
                AddNotification("Request", "Usuário bloquado no sistema.");

                return new AutenticarFuncionarioResponse()
                {
                    Mensagem = "Usuário bloquado, comunique algum administrador do sistema e solicite sua ativação!",
                    Autenticado = false
                };
            }

            if (!string.IsNullOrEmpty(request.TokenPush))
            {
                if (_repositoryPushNotification.Existe(x => x.Token == request.TokenPush && x.Funcionario != null && x.Funcionario.Id == funcionario.Id, x => x.Funcionario) == false)
                {
                    PushNotification push = new PushNotification(funcionario, request.TokenPush);

                    if (IsValid())
                    {
                        _repositoryPushNotification.Adicionar(push);
                    }
                }
            }

            //Cria objeto de resposta
            var response = (AutenticarFuncionarioResponse)funcionario;

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
