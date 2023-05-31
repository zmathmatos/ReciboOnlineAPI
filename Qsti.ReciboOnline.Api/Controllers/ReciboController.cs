using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Qsti.ReciboOnline.Infra.Repositories.Transactions;
using Qsti.ReciboOnline.Domain.Commands.Recibo.ObterRecibo;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao;
using Qsti.ReciboOnline.Domain.Commands.Recibo.ListarRecibo;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Recibo.ListarPor;
using System;
using System.Collections.Generic;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReciboController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ReciboController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected ReciboController()
        {

        }


        [Authorize]
        [HttpGet]
        [Route("api/Recibo/ObterRecibo/{matricula}/{ano}/{mes}")]
        public async Task<IActionResult> ObterRecibo(string matricula, string ano, string mes)
        {
            try
            {
                // Lógica de autenticação e validação

                var request = new ObterReciboRequest()
                {
                   Matricula =  matricula,
                   Ano = ano,
                   Mes = mes,
                };


                // Lógica de manipulação dos dados

                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Tratamento de exceções
                return NotFound(ex.Message);
            }
        }



        /*
                [Authorize]
                [HttpGet]
                [Route("api/Recibo/ObterRecibo/{matricula}")]
                public async Task<IActionResult> ObterRecibo(Guid idFuncionario)
                {

                    try
                    {
                        var request = new ObterReciboRequest(idFuncionario);
                        var result = await _mediator.Send(request, CancellationToken.None);
                        return Ok(result);
                    }
                    catch (System.Exception ex)
                    {
                        //AdiconarLog(ex);

                        return NotFound(ex.Message);
                    }

                }*/

        /* [Authorize]
         [HttpGet]
         [Route("api/Recibo/ListarPor/{matricula}/{ano}/{mes}")] //funcionario
         public async Task<IActionResult> ListarPor(int matricula, int ano, int mes)
         {
             try
             {
                 string funcionarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Funcionario").Value;
                 AutenticarFuncionarioResponse funcionarioResponse = JsonConvert.DeserializeObject<AutenticarFuncionarioResponse>(funcionarioClaims);

                 var request = new ListarPorRequest(matricula, ano, mes);
                 request.SetarMatriculaFuncionario(funcionarioResponse.Matricula);

                 var result = await _mediator.Send(request, CancellationToken.None);
                 return Ok(result);
             }
             catch (System.Exception ex)
             {
                 //AdiconarLog(ex);

                 return NotFound(ex.Message);
             }
         }*/

        [Authorize]
        [HttpGet]
        [Route("api/Recibo/ListarRecibo")]
        public async Task<IActionResult> ListarRecibo()
        {

            try
            {
                string funcionarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Funcionario").Value;
                AutenticarFuncionarioResponse funcionarioResponse = JsonConvert.DeserializeObject<AutenticarFuncionarioResponse>(funcionarioClaims);

                var request = new ListarReciboRequest();
                request.SetarIdFuncionario(funcionarioResponse.Id);

                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }

            

        }
        


        
    }
}
