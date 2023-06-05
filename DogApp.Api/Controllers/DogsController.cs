using System.Net;
using AutoMapper;
using DogApp.Api.Queries;
using DogApp.Application.Commands.Common;
using DogApp.Application.Commands.Dog;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Models;
using DogApp.Domain.Entities;
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
            var paginationCommand = new GetPagedAndSortedDataCommand<DogEntity>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SortingAttribute = query.SortingAttribute,
                SortingOrder = query.SortingOrder
            };

            var dogsByPages = await _mediator.Send(paginationCommand, cancellationToken);

            return dogsByPages.Map(_mapper.Map<List<DogDto>>)
                .Match(dogs =>
                {
                    var pagedResponse = new PagedResponse<List<DogDto>>(dogs);

                    return Ok(pagedResponse);
                }, ExceptionResolver);
        }

        [HttpPost]
        public async Task<ActionResult> AddDog([FromBody] AddDogDto addDogDto, CancellationToken cancellationToken)
        {
            var addDogCommand = _mapper.Map<AddDogCommand>(addDogDto);
            var createdDogId = await _mediator.Send(addDogCommand, cancellationToken);

            return createdDogId.Match(createdId =>
            {
                return StatusCode((int)HttpStatusCode.Created, createdId);
            }, ExceptionResolver);
        }
    }
}
