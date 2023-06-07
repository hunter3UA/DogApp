using System.Net;
using AutoMapper;
using DogApp.Api.Controllers;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Queries;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.Controllers
{
    public class DogsControllerTest
    {
        private readonly DogsController _dogsController;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DogsControllerTest()
        {
            _mediator = A.Fake<IMediator>();
            _mapper = A.Fake<IMapper>();
            _dogsController = new DogsController(_mapper, _mediator);
        }

        [Fact]
        public async Task AddDog_IfValidRequestProvided_ReturnStatusCodeCreated()
        {
            var addDogRequest = new AddDogRequest("Dog", "black", 10, 1);
            var createdDogId = Guid.NewGuid();

            A.CallTo(() => _mapper.Map<AddDogRequest>(A<AddDogDto>._))
                .Returns(addDogRequest);
            A.CallTo(() => _mediator.Send(A<AddDogRequest>._, A<CancellationToken>._))
                .Returns(createdDogId);

            var actionResult = await _dogsController.AddDog(new AddDogDto("Dog", "black", 10, 1), CancellationToken.None);
            var actualResult = actionResult as ObjectResult;

            actualResult.Should().NotBeNull();
            actualResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
            actualResult?.Value.Should().BeEquivalentTo(new { id = createdDogId });
        }

        [Fact]
        public async Task GetDogs_IfValidRequestProvided_ReturnPagedResult()
        {
            var getDogsRequest = new GetDogsRequest(1, 4, "", "");

            var dogDtos = new List<DogDto>
            {
                new DogDto { Name = "Dog1", Color = "black" },
                new DogDto { Name = "Dog2", Color = "black" },
                new DogDto { Name = "Dog3", Color = "black" },
                new DogDto { Name = "Dog4", Color = "black" },
            };

            A.CallTo(() => _mediator.Send(A<GetDogsRequest>._, A<CancellationToken>._))
                .Returns(new List<DogEntity>());
            A.CallTo(() => _mapper.Map<List<DogDto>>(A<List<DogEntity>>._))
                .Returns(dogDtos);

            var actualResult = await _dogsController.GetDogs(new PaginationSortingQuery(), CancellationToken.None);

            actualResult.Should().NotBeNull().And.BeOfType<OkObjectResult>();      
        }
    }
}