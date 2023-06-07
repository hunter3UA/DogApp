using System.Net;
using AutoMapper;
using DogApp.Api.Queries;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Helpers;
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

            var sortedDogRequest = new GetDogsRequest(
                pageSettings.PageNumber,
                pageSettings.PageSize,
                query.SortingOrder,
                query.SortingAttribute);

            var dogs = await _mediator.Send(sortedDogRequest, cancellationToken);

            return CreatePagedResult(dogs, pageSettings.PageNumber, pageSettings.PageSize, totalItems, pageSettings.TotalPages);
        }

        [HttpPost]
        public async Task<ActionResult> AddDog([FromBody] AddDogDto addDogDto, CancellationToken cancellationToken)
        {
            var addDogCommand = _mapper.Map<AddDogRequest>(addDogDto);
            var createdDogId = await _mediator.Send(addDogCommand, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created, new { id = createdDogId });
        }
    }
}