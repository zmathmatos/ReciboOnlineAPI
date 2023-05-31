using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Solicitacao.ListarSolicitacao
{
    public class ListarSolicitacaoResponse
    {
        public Guid Id { get; set; }
        public string Ano { get;  set; }
        public string Mes { get;  set; }
        public string Matricula { get; set; }
        public int TipoFolha { get; set; }
        public EnumStatus Status { get;  set; }
        

        public static explicit operator ListarSolicitacaoResponse(Entities.Solicitacao solicitacao)
        {
            return new ListarSolicitacaoResponse()
            {
                Id = solicitacao.Id,
                Ano = solicitacao.Ano,
                Mes = solicitacao.Mes,
                Matricula = solicitacao.Matricula,
                TipoFolha = solicitacao.TipoFolha,
                Status = solicitacao.Status,
            };
        }
    }
}
