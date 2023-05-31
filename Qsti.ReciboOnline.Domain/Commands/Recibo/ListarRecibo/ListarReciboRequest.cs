using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ListarRecibo
{
    public class ListarReciboRequest : IRequest<Response>
    {
        Guid _idFuncionario;
       
  
        public void SetarIdFuncionario(Guid id)
        {
            _idFuncionario = id;
        }

        public Guid ObterFuncionario()
        {
            return _idFuncionario;
        }


        /*public ListarReciboRequest(string matricula)
        {
            Matricula = matricula;

        }
       
        public string Matricula { get; set; }
       */


    }
}
