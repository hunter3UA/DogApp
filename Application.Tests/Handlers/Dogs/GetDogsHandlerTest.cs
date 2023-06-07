using AutoMapper;
using DogApp.Application.Handlers;
using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Entities;
using DogApp.Domain.Enums;
using FakeItEasy;
using FluentAssertions;
using System.Linq.Expressions;
using Xunit;

namespace Application.Tests.Handlers.Dogs
{
    public class GetDogsHandlerTest
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly GetDogsHandler _handler;

        public GetDogsHandlerTest()
        {
            _repositoryWrapper = A.Fake<IRepositoryWrapper>();
            _mapper = A.Fake<IMapper>();
            _handler = new GetDogsHandler(_repositoryWrapper, _mapper);
        }


        [Fact]
        public async Task Handle_IfEntitiesNotEmpty_ReturnNoneEmptyList()
        {
            var dogEntities = new List<DogEntity>()
            {
                new DogEntity { Name = "Dog1", Color = "Black" },
                new DogEntity { Name = "Dog2", Color = "Black" }
            };

            var fakes = A.CollectionOfFake<DbDog>(2);
            A.CallTo(() => _repositoryWrapper
            .Dogs
            .GetRangeAsync(
                A<CancellationToken>._,
                A<Expression<Func<DbDog, object>>>._,
                A<SortingOrder>._,
                A<int>._,
                A<int>._,
                A<bool>._)).Returns(fakes.ToList());

            A.CallTo(() => _mapper.Map<List<DogEntity>>(A<List<DbDog>>._))
                .Returns(dogEntities);

            var actualResult = await _handler.Handle(new GetDogsRequest(default, default, "", ""), CancellationToken.None);

            actualResult.Should().NotBeNullOrEmpty();
            actualResult.Count().Should().Be(2);
        }
    }
}