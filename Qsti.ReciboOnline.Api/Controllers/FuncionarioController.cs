using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Qsti.ReciboOnline.Api.Security;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AdicionarFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AlterarSenha;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.AutenticarFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.BloquearFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.DesbloquearFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.ListarFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.ObterFuncionarioPorId;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.RemoverFuncionario;
using Qsti.ReciboOnline.Domain.Commands.Funcionario.ResetarSenha;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario;
using Qsti.ReciboOnline.Infra.Repositories.Transactions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Api.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api/Funcionario")]
    //[ApiController]
    public class FuncionarioController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public FuncionarioController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected FuncionarioController()
        {

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Funcionario/Autenticar")]
        public async Task<IActionResult> Autenticar(
           [FromBody]AutenticarFuncionarioRequest request,
           [FromServices]SigningConfigurations signingConfigurations,
           [FromServices]TokenConfigurations tokenConfigurations, [FromServices]  IUnitOfWork unitOfWork)
        {

            try
            {
                var autenticarFuncionarioResponse = await _mediator.Send(request, CancellationToken.None);

                if (autenticarFuncionarioResponse.Autenticado == true)
                {
                    var response = GerarToken(autenticarFuncionarioResponse, signingConfigurations, tokenConfigurations);

                    unitOfWork.SaveChanges();

                    return Ok(response);

                }

                return Ok(autenticarFuncionarioResponse);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Funcionario/Listar")]
        public async Task<IActionResult> ListarFuncionario()
        {
            try
            {
                var request = new ListarFuncionarioRequest();
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
        [Route("api/Funcionario/Adicionar")]
        public async Task<IActionResult> AdicionarFuncionario([FromBody]AdicionarFuncionarioRequest request)
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
        [Route("api/Funcionario/Alterar")]
        public async Task<IActionResult> AlterarFuncionario([FromBody]AlterarFuncionarioRequest request)
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
        [Route("api/Funcionario/Excluir/{id:Guid}")]
        public async Task<IActionResult> RemoverFuncionario(Guid id)
        {
            try
            {
                var request = new ExcluirFuncionarioResquest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/Funcionario/Bloquear")]
        public async Task<IActionResult> BloquearFuncionario([FromBody]BloquearFuncionarioRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarFuncionarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarFuncionarioResponse>(usuarioClaims);

                request.IdUsuario = usuarioResponse.Id;

                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/Funcionario/Desbloquear")]
        public async Task<IActionResult> DesbloquearFuncionario([FromBody]DesbloquearFuncionarioRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarFuncionarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarFuncionarioResponse>(usuarioClaims);

                request.IdUsuario = usuarioResponse.Id;

                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }
        private object GerarToken(AutenticarFuncionarioResponse response, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            if (response.Autenticado == true)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        //new Claim(JwtRegisteredClaimNames.UniqueName, response.Usuario)
                        new Claim("Funcionario", JsonConvert.SerializeObject(response))
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK",
                    Nome = response.Nome
                };
            }
            else
            {
                return response;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Funcionario/ResetarSenha")]
        public async Task<IActionResult> ResetarSenha([FromBody]ResetarSenhaRequest request)
        {
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);
            }



        }

        [AllowAnonymous]
        [HttpPut]
        [Route("api/Funcionario/AlterarSenha")]
        public async Task<IActionResult> AlterarSenha([FromBody]AlterarSenhaRequest request)
        {
            try
            {
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
        [Route("api/Funcionario/obterPorId/{id:Guid}")]
        public async Task<IActionResult> ObterFuncionarioPorIdRequest(Guid id)
        {
            try
            {
                var request = new ObterFuncionarioPorIdRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
