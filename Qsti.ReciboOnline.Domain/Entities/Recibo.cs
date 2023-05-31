using Qsti.ReciboOnline.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class Recibo : EntityBase
    {
        protected Recibo()
        {

        }

        public Recibo(string matricula, string ano, string mes, int tipoFolha, string cnpj, string razaoSocial, string logradouro, int numero,
                      string cidade, string uf, string nome, string funcao, string centroCusto,
                      string contaCorrente, int codigoPD, string descricaoCodigoPD, string referencia, double valor,
                      string descricaoTipo, double salarioBase, double totalProventos, double totalDescontos,
                      double totalLiquido, double fgtsMes, double baseFGTS, double baseINSS, double baseIR,
                      double baseIRFerias, int ordemTipo, bool status, bool visualizado
                      )
        {
            Matricula = matricula;
            Ano = ano;
            Mes = mes;
            TipoFolha = tipoFolha;
            CNPJ = cnpj;
            RazaoSocial = razaoSocial;
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uf;
            Nome = nome;
            Funcao = funcao;
            CentroCusto = centroCusto;
            ContaCorrente = contaCorrente;
            CodigoPd = codigoPD;
            DescricaoCodigoPD = descricaoCodigoPD;
            Referencia = referencia;
            Valor = valor;
            DescricaoTipo = descricaoTipo;
            SalarioBase = salarioBase;
            TotalProventos = totalProventos;
            TotalDescontos = totalDescontos;
            TotalLiquido = totalLiquido;
            FGTSMes = fgtsMes;
            BaseFGTS = baseFGTS;
            BaseINSS = baseINSS;
            BaseIR = baseIR;
            BaseIRFerias = baseIRFerias;
            OrdemTipo = ordemTipo;
            Status = status;
            Visualizado = visualizado;

        }

        public string Matricula { get; set; }
        public string Ano { get; set; }
        public string Mes { get; set; }
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
    }
}

