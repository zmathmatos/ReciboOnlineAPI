using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using System;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ListarRecibo
{
    public class ListarReciboResponse
    {
       
        public string Ano { get; set; }
        public string Mes { get; set; }
        public string Matricula { get; set; }
        public int TipoFolha { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string CentroCusto { get; set; }
        public string ContaCorrente { get; set; }
        public int CodigoPd { get; set; }
        public string DescricaoCodigoPD { get; set; }
        public string Referencia { get; set; }
        public double Valor { get; set; }
        public string TipoCodigo { get; set; }
        public string DescricaoTipo { get; set; }
        public double SalarioBase { get; set; }
        public double TotalProventos { get; set; }
        public double TotalDescontos { get; set; }
        public double TotalLiquido { get; set; }
        public double FGTSMes { get; set; }
        public double BaseFGTS { get; set; }
        public double BaseINSS { get; set; }
        public double BaseIR { get; set; }
        public double BaseIRFerias { get; set; }
        public int OrdemTipo { get; set; }
        public bool Status { get; set; }
        public bool Visualizado { get; set; }
       

        public static explicit operator ListarReciboResponse(Entities.Recibo recibo)
        {
            return new ListarReciboResponse()
            {
                Ano = recibo.Ano,
                Mes = recibo.Mes,
                Matricula = recibo.Matricula,
                TipoFolha = recibo.TipoFolha,
                CNPJ = recibo.CNPJ,
                RazaoSocial = recibo.RazaoSocial,
                Logradouro = recibo.Logradouro,
                Numero = recibo.Numero,
                Cidade = recibo.Cidade,
                UF = recibo.UF,
                Nome = recibo.Nome,
                Funcao = recibo.Funcao,
                CentroCusto = recibo.CentroCusto,
                ContaCorrente = recibo.ContaCorrente,
                CodigoPd = recibo.CodigoPd,
                DescricaoCodigoPD = recibo.DescricaoCodigoPD,
                Referencia = recibo.Referencia,
                Valor = recibo.Valor,
                TipoCodigo = recibo.TipoCodigo,
                DescricaoTipo = recibo.DescricaoTipo,
                SalarioBase = recibo.SalarioBase,
                TotalProventos = recibo.TotalProventos,
                TotalDescontos = recibo.TotalDescontos,
                TotalLiquido = recibo.TotalLiquido,
                FGTSMes = recibo.FGTSMes,
                BaseFGTS = recibo.BaseFGTS,
                BaseINSS = recibo.BaseINSS,
                BaseIR = recibo.BaseIR,
                BaseIRFerias = recibo.BaseIRFerias,
                OrdemTipo = recibo.OrdemTipo,
                Status = recibo.Status,
                Visualizado = recibo.Visualizado,


            };
        }
    }
}
