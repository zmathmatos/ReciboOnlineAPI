using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarFuncionario;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarFuncionario
{
    public class AlterarFuncionarioHandler : Notifiable, IRequestHandler<AlterarFuncionarioRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;


        public AlterarFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
   
        }

        public async Task<Response> Handle(AlterarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Funcionario"));
                return new Response(this);
            }


            Entities.Funcionario funcionario = _repositoryFuncionario.ObterPorId(request.Id);

            if (funcionario == null)
            {
                AddNotification("Funcionario", MSG.X0_NAO_INFORMADO.ToFormat("Funcionario"));
                return new Response(this);
            }


            bool emailExistente = _repositoryFuncionario.Existe(x => x.Email == request.Email && x.Id != request.Id);

            if (emailExistente==true)
            {
                AddNotification("Email", MSG.ESTE_X0_JA_EXISTE.ToFormat("Email"));
                return new Response(this);
            }


            funcionario.AlterarFuncionario(request.Nome, request.Email);

            AddNotifications(funcionario);

            if (IsInvalid())
            {
                return new Response(this);
            }

            funcionario = _repositoryFuncionario.Editar(funcionario);

            //Criar meu objeto de resposta
            var response = new Response(this, funcionario);

            return await Task.FromResult(response);
        }
    }
}
