using AutoMapper;
using Cards.API.Models.Attribute;
using Cards.API.Models.Common;
using Cards.API.Models.DTOs.Requests.CardModule;
using Cards.API.Models.DTOs.Responses.CardModule;
using Core.Domain.Entities.CardModule.Aggregates;
using Core.Domain.Utils;
using Core.Management.Interfaces.CardModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;

namespace Cards.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("v{version:apiVersion}/cards"), SwaggerOrder("B")]
    [Authorize(Policy = nameof(AuthPolicy.GlobalRights))]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ICardRepository _cardRepository;

        /// <summary>
        /// Business Posting Group Constructor
        /// </summary>
        public CardsController(IMapper mapper,
           IConfiguration configuration, ICardRepository cardRepository)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            _cardRepository = cardRepository;
        }

        /// <summary>
        /// Used to create Cards
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("create")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<MinifiedCardDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> RegisterBusinessPostingGroup([FromBody, Required] CardRequest request)
        {

            return Created(string.Empty, new ResponseObject<MinifiedCardDto>
            {
                Data = new[] { mapper.Map<MinifiedCardDto>(await _cardRepository.CreateCard(request.UserId,request.Name, request.Description, request.Color, request.CreatedBy)) }
            });
        }

        /// <summary>
        /// Used to edit a Card
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut, Route("edit")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<bool>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> EditBusinessPostingGroup([FromBody, Required] EditCardRequest request)
        {
            return Ok(new ResponseObject<bool>
            {
                Data = new[] { await _cardRepository.EditCard(long.Parse(request.CardId), long.Parse(request.UserId), request.Name, request.Description, request.Color, request.RecordStatus, request.ModifiedBy) }
            });
        }

        /// <summary>
        /// Get Card by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseObject<CardDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetBusinessPostingGroupById([FromRoute, Range(1, long.MaxValue, ErrorMessage = "id must be greater than 0"),
            Required(AllowEmptyStrings = false, ErrorMessage = "id must be provided")]
            string id)
        {
            Card card = await _cardRepository.GetCardById(long.Parse(id));

            return Ok(new ResponseObject<CardDto> { Data = card is null ? Enumerable.Empty<CardDto>() : new[] { mapper.Map<CardDto>(card) } });
        }

        /// <summary>
        /// Retrieve paginated list of Cards for Admins
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="offset"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Route("cards")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PageCollectionInfo<CardDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetBusinessPostingGroups([FromQuery] string searchTerm, [FromQuery] int offset, [FromQuery] int pageSize)
        {
            offset = offset < 1 ? 0 : offset; pageSize = pageSize < 1 ? Convert.ToInt32(configuration["Paging:Size"]) : pageSize;


            (List<Card> cards, int newStartIndex, int newPageSize, int totalCount) = await _cardRepository.GetCards(searchTerm, offset, pageSize);
            return Ok(new PageCollectionInfo<CardDto>
            {
                PageCollection = mapper.Map<List<CardDto>>(cards),
                ItemsCount = totalCount,
                PageIndex = offset,
                PageSize = newPageSize
            });
        }

        /// <summary>
        /// Retrieve paginated list of Cards by User
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="userId"></param>
        /// <param name="offset"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Route("cardsByUser")]
        [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PageCollectionInfo<CardDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetBusinessPostingGroupsByCompanyAndPartnerId([FromQuery] string userId, [FromQuery] string searchTerm, [FromQuery] int offset, [FromQuery] int pageSize)
        {
            offset = offset < 1 ? 0 : offset; pageSize = pageSize < 1 ? Convert.ToInt32(configuration["Paging:Size"]) : pageSize;


            (List<Card> businessPostingGroups, int newStartIndex, int newPageSize, int totalCount) = await _cardRepository.GetCardsByUser(searchTerm, offset, pageSize, long.Parse(userId));
            return Ok(new PageCollectionInfo<CardDto>
            {
                PageCollection = mapper.Map<List<CardDto>>(businessPostingGroups),
                ItemsCount = totalCount,
                PageIndex = offset,
                PageSize = newPageSize
            });
        }
    }
}
