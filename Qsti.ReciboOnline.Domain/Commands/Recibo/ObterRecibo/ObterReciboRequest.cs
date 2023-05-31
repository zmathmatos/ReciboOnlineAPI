using MediatR;
using System;
using System.Collections.Generic;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ObterRecibo
{
    public class ObterReciboRequest : IRequest<Response>
    {

        public string Matricula { get; set; }
        public string Ano { get; set; }
        public string Mes { get; set; }
        public Guid IdRecibo { get; set; }

        private Guid _idFuncionario;

        public void SetarIdFuncionario(Guid id)
        {
            _idFuncionario = id;
        }
        
        public Guid ObterIdFuncionario()
        {
            return _idFuncionario;
        }
    }
}
