using Amazon.Auth.AccessControlPolicy;
using AutoMapper;
using Cards.API.Models.Attribute;
using Cards.API.Models.Common;
using Cards.API.Models.DTOs.Requests.CardModule;
using Cards.API.Models.DTOs.Responses.CardModule;
using Core.Domain.Entities.CardModule.Aggregates;
using Core.Domain.Utils;
using Core.Management.Interfaces.CardModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Net;
using Core.Management.Interfaces.AdminModule;
using Cards.API.Models.DTOs.Requests.AdminModule;
using Cards.API.Models.DTOs.Responses.AdminModule;
using Core.Domain.Entities.UserModule.Aggregates;

namespace Cards.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v{version:apiVersion}/users"), SwaggerOrder("C")]
    [Authorize(Policy = nameof(AuthPolicy.GlobalRights))]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Business Posting Group Constructor
        /// </summary>
        public UsersController(IMapper mapper,
           IConfiguration configuration, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Used to create Users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("create")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<MinifiedUserDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> RegisterUser([FromBody, Required] UserRequest request)
        {

            return Created(string.Empty, new ResponseObject<MinifiedUserDto>
            {
                Data = new[] { mapper.Map<MinifiedUserDto>(await _userRepository.CreateUser(request.Email, request.Role, request.Password, request.CreatedBy)) }
            });
        }

        /// <summary>
        /// Used to edit a User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut, Route("edit")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<bool>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> EditUser([FromBody, Required] EditUserRequest request)
        {
            return Ok(new ResponseObject<bool>
            {
                Data = new[] { await _userRepository.EditUser(long.Parse(request.UserId), request.Email, request.Role, request.Password, request.ModifiedBy) }
            });
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<UserDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetUserById([FromRoute, Range(1, long.MaxValue, ErrorMessage = "id must be greater than 0"),
            Required(AllowEmptyStrings = false, ErrorMessage = "id must be provided")]
            string id)
        {
            User user = await _userRepository.GetUserById(long.Parse(id));

            return Ok(new ResponseObject<UserDto> { Data = user is null ? Enumerable.Empty<UserDto>() : new[] { mapper.Map<UserDto>(user) } });
        }

        /// <summary>
        /// Retrieve paginated list of Users
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="offset"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Route("users")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PageCollectionInfo<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers([FromQuery] string searchTerm, [FromQuery] int offset, [FromQuery] int pageSize)
        {
            offset = offset < 1 ? 0 : offset; pageSize = pageSize < 1 ? Convert.ToInt32(configuration["Paging:Size"]) : pageSize;

            (List<User> users, int newStartIndex, int newPageSize, int totalCount) = await _userRepository.GetUsers(searchTerm, offset, pageSize);
            return Ok(new PageCollectionInfo<UserDto>
            {
                PageCollection = mapper.Map<List<UserDto>>(users),
                ItemsCount = totalCount,
                PageIndex = offset,
                PageSize = newPageSize
            });
        }
    }
}
