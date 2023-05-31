using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Qsti.ReciboOnline.Domain.Resources;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Domain.Commands.Recibo.ObterRecibo
{
    public class ObterReciboHandler : Notifiable, IRequestHandler<ObterReciboRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryRecibo _repositoryRecibo;
        private readonly IRepositoryFuncionario _repositoryFuncionario;
        private readonly IRepositorySolicitacao _repositorySolicitacao;

        public ObterReciboHandler(IMediator mediator, IRepositoryRecibo repositoryRecibo, IRepositoryFuncionario repositoryFuncionario, IRepositorySolicitacao repositorySolicitacao)
        {
            _mediator = mediator;
            _repositoryRecibo = repositoryRecibo;
            _repositoryFuncionario = repositoryFuncionario;
            _repositorySolicitacao = repositorySolicitacao;
        }

        public async Task<Response> Handle(ObterReciboRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

           var recibo = _repositoryRecibo.Listar(x => x.Matricula, x => x.Ano, x => x.Mes,x => x.CNPJ,x => x.RazaoSocial,x => x.Logradouro, x => x.Numero,x => x.Cidade,x => x.UF,x => x.Nome,x => x.Funcao,
                x => x.CentroCusto,x => x.ContaCorrente,x => x.CodigoPd,x => x.DescricaoCodigoPD,x => x.Referencia,x => x.Valor,x => x.TipoCodigo,x => x.DescricaoTipo,x => x.SalarioBase,x => x.TotalProventos,
                x => x.TotalDescontos,x => x.TotalLiquido,x => x.FGTSMes,x => x.BaseFGTS,x => x.BaseINSS,x => x.BaseIR,x => x.BaseIRFerias, x => x.OrdemTipo,x => x.Status,x => x.Visualizado).Where(x => x.Matricula == request.Matricula && x.Ano == request.Ano && x.Mes == request.Mes);



            var funcionario = _repositoryFuncionario.ObterPorId(request.ObterIdFuncionario());

            var result = new
            {
                Recibo = recibo.Select(x => new
                {
                    Matricula = x.Matricula,
                    Ano = x.Ano,
                    Mes = x.Mes,
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

                })
                
            };


            //Cria objeto de resposta
            var response = new Response(this, result);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
