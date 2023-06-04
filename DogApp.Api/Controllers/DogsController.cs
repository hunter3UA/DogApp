using AutoMapper;
using DogApp.Application.Commands.Common;
using DogApp.Application.Commands.Dog;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Queries;
using DogApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
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
            var paginationCommand = new GetPagedAndSortedDataCommand<DogEntity>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortingAttribute = query.SortingAttribute,
                SortingOrder = query.SortingOrder,
            };

            var pagedResult = await _mediator.Send(paginationCommand, cancellationToken);

            return pagedResult.Match<ActionResult>(obj => { return new OkObjectResult(obj); }, error => { return new BadRequestObjectResult(error); });
        }

        [HttpPost]
        public async Task<ActionResult> AddDog([FromBody] AddDogDto addDogDto, CancellationToken cancellationToken)
        {
            var addDogCommand = _mapper.Map<AddDogCommand>(addDogDto);
            var addedId = await _mediator.Send(addDogCommand, cancellationToken);

            return Ok();
        }

    }
}
