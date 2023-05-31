using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Commands.Recibo.ListarPor;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ListarParaFuncionarioPor
{
    public class ListarPorHandler : Notifiable, IRequestHandler<ListarPorRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryRecibo _repositoryRecibo;
        private readonly IRepositoryFuncionario _repositoryFuncionario;

        public ListarPorHandler(IMediator mediator, IRepositoryRecibo repositoryRecibo, IRepositoryFuncionario repositoryFuncionario)
        {
            _mediator = mediator;
            _repositoryRecibo = repositoryRecibo;
            _repositoryFuncionario = repositoryFuncionario;
        }

        public async Task<Response> Handle(ListarPorRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var funcionario = _repositoryRecibo.ObterPorId(request.ObterIdFuncionario());

            var reciboCollection = _repositoryRecibo.ListarPor(x => x.Matricula == funcionario.Matricula);


            //Devolve dados de forma customizada
            var result = reciboCollection.Select(x => new {
                Ano = x.Ano,
                Mes = x.Mes,
                Matricula = x.Matricula,
                CNPJ = x.CNPJ,
                RazaoSocial = x.RazaoSocial,
                Logradouro = x.Logradouro,
                Numero = x.Numero,
                Cidade = x.Cidade,
                UF = x.UF,
                Nome = x.Nome,
                Funcao = x.Funcao,
                CentroCusto = x.CentroCusto,
                ContaCorrente = x.ContaCorrente,
                CodigoPd = x.CodigoPd,
                DescricaoCodigoPD = x.DescricaoCodigoPD,
                Referencia = x.Referencia,
                Valor = x.Valor,
                TipoCodigo = x.TipoCodigo,
                DescricaoTipo = x.DescricaoTipo,
                SalarioBase = x.SalarioBase,
                TotalProventos = x.TotalProventos,
                TotalDescontos = x.TotalDescontos,
                TotalLiquido = x.TotalLiquido,
                FGTSMes = x.FGTSMes,
                BaseFGTS = x.BaseFGTS,
                BaseINSS = x.BaseINSS,
                BaseIR = x.BaseIR,
                BaseIRFerias = x.BaseIRFerias,
                OrdemTipo = x.OrdemTipo,
                Status = x.Status,
                Visualizado = x.Visualizado,
            }).ToList();
            //Cria objeto de resposta
            var response = new Response(this, result);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }

    }
}
