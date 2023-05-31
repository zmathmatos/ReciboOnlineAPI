using prmToolkit.NotificationPattern;
using Qsti.ReciboOnline.Domain.Entities.Base;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using System;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class Solicitacao : EntityBase
    {
        protected Solicitacao()
        {

        }

        public Solicitacao(Usuario usuario, string matricula, int tipoFolha, string ano, string mes)
        {

            Usuario = usuario;
            Ano = ano;
            Mes = mes;
            Matricula = matricula;
            TipoFolha = tipoFolha;
            Status = EnumStatus.Disponivel;
           
           
        }

    
        public Usuario Usuario { get; set; }
        public string Ano { get;  set; }
        public string Mes { get;  set; }
        public int TipoFolha { get; set; }
        public string Matricula { get; set; }
        public EnumStatus Status { get; set; }

        public void AlterarStatus(EnumStatus status)
        {
            Status = status;
        }
    }
}
