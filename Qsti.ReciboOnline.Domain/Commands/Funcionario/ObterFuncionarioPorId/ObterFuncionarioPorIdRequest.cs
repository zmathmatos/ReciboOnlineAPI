using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ObterFuncionarioPorId
{
    public class ObterFuncionarioPorIdRequest : IRequest<Response>
    {
        public ObterFuncionarioPorIdRequest(Guid idFuncionario)
        {
            IdFuncionario = idFuncionario;
        }

        public Guid IdFuncionario { get; set; }
    }
}
