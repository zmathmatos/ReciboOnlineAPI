using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Qsti.ReciboOnline.Domain.Commands.Empresa.AdicionarEmpresa;
using Qsti.ReciboOnline.Domain.Commands.Empresa.AlterarEmpresa;
using Qsti.ReciboOnline.Domain.Commands.Empresa.ExcluirEmpresa;
using Qsti.ReciboOnline.Domain.Commands.Empresa.ListarEmpresa;
using Qsti.ReciboOnline.Infra.Repositories.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Api.Controllers
{
    public class EmpresaController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public EmpresaController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected EmpresaController()
        {

        }

        [Authorize]
        [HttpGet]
        [Route("api/Empresa/Listar")]
        public async Task<IActionResult> ListarEmpresa()
        {
            try
            {
                var request = new ListarEmpresaRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/Empresa/Adicionar")]
        public async Task<IActionResult> AdicionarEmpresa([FromBody] AdicionarEmpresaRequest request)
        {
            try
            {
                //string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                //AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                //request.IdUsuario = usuarioResponse.Id;

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
        [HttpPut]
        [Route("api/Empresa/Alterar")]
        public async Task<IActionResult> AlterarEmpresa([FromBody] AlterarEmpresaRequest request)
        {
            try
            {
                //string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                //AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                //request.IdUsuario = usuarioResponse.Id;

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
        [HttpDelete]
        [Route("api/Empresa/Excluir/{id:Guid}")]
        public async Task<IActionResult> RemoverEmpresa(Guid id)
        {
            try
            {
                var request = new ExcluirEmpresaResquest(id);
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