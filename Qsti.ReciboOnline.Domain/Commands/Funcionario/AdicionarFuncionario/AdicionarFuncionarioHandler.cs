using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.AdicionarFuncionario
{
    public class AdicionarFuncionarioHandler : Notifiable, IRequestHandler<AdicionarFuncionarioRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
       

        public AdicionarFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
            
        }

        public async Task<Response> Handle(AdicionarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            //Validar se o requeste veio preenchido
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));

                return new Response(this);
            }

            //verifica se a matrícula já existe
            if (_repositoryFuncionario.Existe(x => x.Matricula == request.Matricula))
            {
                AddNotification("Matricula", MSG.ESTA_X0_JA_EXISTE.ToFormat("Matricula"));
                return new Response(this);
            }

            Entities.Funcionario funcionario = new Entities.Funcionario(request.Matricula, request.Nome, request.Email, request.Senha);

            AddNotifications(funcionario);

            if (IsInvalid())
            {
                return new Response(this);
            }

            funcionario = _repositoryFuncionario.Adicionar(funcionario);

            //Criar meu objeto de resposta
            var response = new Response(this, funcionario);

            //Dispara uma notificação que um novo funcionario foi cadastrado
            Funcionario.AdicionarFuncionario.AdicionarFuncionarioNotification adicionarFuncionarioNotification = new Funcionario.AdicionarFuncionario.AdicionarFuncionarioNotification(funcionario.Id, funcionario.Nome);

            await _mediator.Publish(adicionarFuncionarioNotification);

            return await Task.FromResult(response);
        }
    }
}
