using MediatR;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.RemoverFuncionario
{
    public class ExcluirFuncionarioResquest : IRequest<Response>
    {
        public ExcluirFuncionarioResquest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
