using AutoMapper;

using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Utils;
using Core.Management.Interfaces.AdminModule;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cards.API.Models.Attribute;
using Cards.API.Models.Common;
using Cards.API.Models.DTOs.Requests.Auth;
using Cards.API.Models.DTOs.Responses.Auth;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;

namespace Cards.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v{version:apiVersion}/auth"), SwaggerOrder("A")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISecurityRepository securityRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="securityRepository"></param>
        public AuthController(IMapper mapper, ISecurityRepository securityRepository)
        {
            this.mapper = mapper;
            this.securityRepository = securityRepository;
        }

        /// <summary>
        /// Offers ability to register api clients
        /// </summary>     
        /// <param name="request"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("client")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<MinifiedClientDto>), (int)HttpStatusCode.OK)]
        [Authorize(Policy = nameof(AuthPolicy.ElevatedRights))]
        public async Task<IActionResult> RegisterClient([FromBody, Required] ClientRequest request)
        {
            Client client = await securityRepository.CreateClient(request.Name, request.ContactEmail, request.Description);

            return Ok(new ResponseObject<MinifiedClientDto> { Data = new[] { mapper.Map<MinifiedClientDto>(client) } });
        }

        /// <summary>
        /// Before invoking this endpoint ensure your key and secret are whitelisted. 
        /// Generates a JWT Bearer access token that can be used to authorize subsequent requests.      
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, Route("token")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<TokenDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateToken([FromBody, Required] TokenRequest request)
        {
            Client client = await securityRepository.AuthenticateClient(request.ApiKey, request.AppSecret);
            if (client is null) return Forbid();

            (string token, long expires) = securityRepository.CreateAccessToken(client: client);

            return Ok(new ResponseObject<TokenDto> { Data = new[] { new TokenDto { AccessToken = token, Expires = expires, TokenType = "Bearer" } } });
        }

        /// <summary>
        /// Extends the lifetime of an accessToken before it expires
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("token/refresh")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<TokenDto>), (int)HttpStatusCode.OK)]
        [Authorize(Policy = nameof(AuthPolicy.GlobalRights))]
        public async Task<IActionResult> RefreshAccessToken([FromBody, Required] RefreshRequest request)
        {
            string bearerToken = await HttpContext.GetTokenAsync("access_token");
            (string token, long expires) = await securityRepository.ExtendAccessTokenLifetime(bearerToken, request.AppSecret);

            return Ok(new ResponseObject<TokenDto> { Data = new[] { new TokenDto { AccessToken = token, Expires = expires, TokenType = "Bearer" } } });
        }

        /// <summary>
        /// Activates and assigns desired role to a client after verification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("client/activate")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<ClientDto>), (int)HttpStatusCode.OK)]
        [Authorize(Policy = nameof(AuthPolicy.ElevatedRights))]
        public async Task<IActionResult> AssignPlusActivateClientRole([FromBody, Required] ActivationRequest request)
        {
            Client client = await securityRepository.AssignClientRole(request.ApiKey, (Roles)request.Role);
            return Ok(new ResponseObject<ClientDto> { Data = client is null ? Enumerable.Empty<ClientDto>() : new[] { mapper.Map<ClientDto>(client) } });
        }

        /// <summary>
        /// Allows fetching of a resource client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("getClientById/{id}")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<ClientDto>), (int)HttpStatusCode.OK)]
        [Authorize(Policy = nameof(AuthPolicy.ElevatedRights))]
        public async Task<IActionResult> GetClientById([FromRoute, Required] Guid id)
        {
            Client client = await securityRepository.GetClientById(id);
            return Ok(new ResponseObject<ClientDto> { Data = client is null ? Enumerable.Empty<ClientDto>() : new[] { mapper.Map<ClientDto>(client) } });
        }

        /// <summary>
        /// Allows fetching of resource clients
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, Route("getClients")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<ClientDto>), (int)HttpStatusCode.OK)]
        [Authorize(Roles = nameof(Roles.Root))]
        public async Task<IActionResult> GetClients()
        {
            return Ok(new ResponseObject<ClientDto> { Data = mapper.Map<List<ClientDto>>(await securityRepository.GetClients()) });
        }
    }
}
