using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ListarFuncionario
{
    public class ListarFuncionarioHandler : Notifiable, IRequestHandler<ListarFuncionarioRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryFuncionario _repositoryFuncionario;

        public ListarFuncionarioHandler(IMediator mediator, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryFuncionario = repositoryFuncionario;
        }

        public async Task<Response> Handle(ListarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var funcionarioCollection = _repositoryFuncionario.Listar(x => x.UsuarioBloqueio).OrderBy(x => x.Nome).ToList();

            //Devolve dados de forma customizada evitando passar a senha
            var result = funcionarioCollection.Select(x => new { Id = x.Id, Matricula = x.Matricula, Nome = x.Nome, Email = x.Email, DataBloqueio = x.DataBloqueio, Motivo = x.Motivo, Status = x.Status, DataCadastro = x.DataCadastro, NomeFuncionarioBloqueio = x.UsuarioBloqueio?.Nome, NomeUsuarioDesbloqueio = x.UsuarioDesbloqueio?.Nome, DataDesbloqueio = x.DataDesbloqueio });

            //Cria objeto de resposta
            var response = new Response(this, result);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }


    }
}
