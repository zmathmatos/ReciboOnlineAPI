using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Qsti.ReciboOnline.Api.Security;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AdicionarUsuario;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AlterarSenha;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario;
using Qsti.ReciboOnline.Domain.Commands.Usuario.ListarUsuario;
using Qsti.ReciboOnline.Domain.Commands.Usuario.MudarStatus;
using Qsti.ReciboOnline.Domain.Commands.Usuario.ResetarSenha;
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
    //[Route("api/Usuario")]
    //[ApiController]
    public class UsuarioController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected UsuarioController()
        {

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuario/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarUsuarioRequest request)
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
        [HttpPost]
        [Route("api/Usuario/ResetarSenha")]
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
        [Route("api/Usuario/AlterarSenha")]
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

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuario/Autenticar")]
        public async Task<IActionResult> Autenticar(
           [FromBody]AutenticarUsuarioRequest request,
           [FromServices]SigningConfigurations signingConfigurations,
           [FromServices]TokenConfigurations tokenConfigurations, [FromServices] IUnitOfWork unitOfWork)
        {
            try
            {
                var autenticarUsuarioResponse = await _mediator.Send(request, CancellationToken.None);

                if (autenticarUsuarioResponse.Autenticado == true)
                {
                    var response = GerarToken(autenticarUsuarioResponse, signingConfigurations, tokenConfigurations);

                    unitOfWork.SaveChanges();
                    return Ok(response);
                }

                return Ok(autenticarUsuarioResponse);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Usuario/Listar")]
        public async Task<IActionResult> ListarConfiguracao()
        {
            try
            {
                var request = new ListarUsuarioRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }
        private object GerarToken(AutenticarUsuarioResponse response, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            if (response.Autenticado == true)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        //new Claim(JwtRegisteredClaimNames.UniqueName, response.Usuario)
                        new Claim("Usuario", JsonConvert.SerializeObject(response))
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

        [Authorize]
        [HttpPut]
        [Route("api/Usuario/MudarStatus")]
        public async Task<IActionResult> BloquearUsuario([FromBody]MudarStatusRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                request.SetarUsuarioBloqueio(usuarioResponse.Id);

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
