using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ListarPor
{
    public class ListarPorRequest : IRequest<Response>
    {
        Guid _idFuncionario;
        public ListarPorRequest(int matricula, int ano, int mes )
        {
            Matricula = matricula;
            Ano = ano;
            Mes = mes;
        }

        
        public void SetarMatriculaFuncionario(Guid id)
        {
            _idFuncionario = id;
        }

        public Guid ObterIdFuncionario()
        {
            return _idFuncionario;
        }

        public int Matricula { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
    }
}
