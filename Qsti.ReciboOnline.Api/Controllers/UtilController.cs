using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Qsti.ReciboOnline.Api.Security;
using Qsti.ReciboOnline.Domain.Commands.Usuario.AutenticarUsuario;
using Qsti.ReciboOnline.Domain.Commands.Util.EnviarAvisoPush;
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
    public class UtilController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UtilController(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected UtilController()
        {

        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Util/EnviarAvisoPush")]
        public async Task<IActionResult> EnviarAvisoPush([FromBody] EnviarAvisoPushRequest request)
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
    }
}
