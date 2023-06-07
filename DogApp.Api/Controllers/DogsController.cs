using System.Net;
using AutoMapper;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Helpers;
using DogApp.Application.Queries;
using DogApp.Application.Requests.Dog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DogsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetDogs([FromQuery] PaginationSortingQuery query, CancellationToken cancellationToken)
        {
            int totalItems = await _mediator.Send(new GetCountOfDogsRequest(), cancellationToken);
            var pageSettings = PaginationHelper.FilterSettings(query.PageNumber, query.PageSize, totalItems);

            var getDogsRequest = new GetDogsRequest(
                pageSettings.PageNumber,
                pageSettings.PageSize,
                query.SortingOrder,
                query.SortingAttribute);

            var dogs = await _mediator.Send(getDogsRequest, cancellationToken);
            var dogsDtos = _mapper.Map<List<DogDto>>(dogs);

            return CreatePagedResult(dogsDtos, pageSettings.PageNumber, pageSettings.PageSize, totalItems, pageSettings.TotalPages);
        }

        [HttpPost]
        public async Task<ActionResult> AddDog([FromBody] AddDogDto addDogDto, CancellationToken cancellationToken)
        {
            var addDogRequest = _mapper.Map<AddDogRequest>(addDogDto);
            var createdDogId = await _mediator.Send(addDogRequest, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created, new { id = createdDogId });
        }
    }
}