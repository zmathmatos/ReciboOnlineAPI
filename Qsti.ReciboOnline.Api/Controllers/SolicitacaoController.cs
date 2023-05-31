using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.AlterarStatusDaSolicitacao;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.ExcluirSolicitacao;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.ListarSolicitacao;
using Qsti.ReciboOnline.Domain.Commands.Solicitacao.SolicitarPublicacao;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario;
using Qsti.ReciboOnline.Domain.Enums.Solicitacao;
using Qsti.ReciboOnline.Infra.Repositories.Transactions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Api.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api/Cliente")]
    //[ApiController]
    public class SolicitacaoController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public SolicitacaoController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected SolicitacaoController()
        {

        }


        [Authorize]
        [HttpPost]
        [Route("api/Solicitacao/SolicitarPublicacao")]
        public async Task<IActionResult> AdicionarSolicitacao([FromBody] SolicitarPublicacaoRequest request)
        {
            try
            {
                string claims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(claims);
                request.SetUsuario(usuarioResponse.Id);

                var response = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/Solicitacao/Listar/{status}")]
        public async Task<IActionResult> ListarSolicitacao(EnumStatus status)
        {
            try
            {
                string claims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(claims);

                var request = new ListarSolicitacaoRequest(status);
                request.SetUsuario(usuarioResponse.Id);

                var response = await _mediator.Send(request, CancellationToken.None);
                //return await ResponseAsync(response);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("api/Solicitacao/AlterarStatus/{idSolicitacao:Guid}/{status}")]
        public async Task<IActionResult> AlterarStatusDaSolicitacao(Guid idSolicitacao, EnumStatus status)
        {
            try
            {
                var request = new AlterarStatusDaSolicitacaoRequest(idSolicitacao, status);
                var response = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(response);

                //return Ok(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);

            }

        }



        [Authorize]
        [HttpDelete]
        [Route("api/Solicitacao/Excluir/{id:Guid}")]
        public async Task<IActionResult> ExcluirSolicitacao(Guid id)
        {
            try
            {
                var request = new ExcluirSolicitacaoRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }
    }
}
