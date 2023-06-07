using AutoMapper;
using DogApp.Application.Handlers;
using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.DbEntities;
using FakeItEasy;
using FluentAssertions;
using System.Linq.Expressions;
using Xunit;

namespace Application.Tests.Handlers.Dogs
{
    public class AddDogHandlerTest
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly AddDogHandler _handler;

        public AddDogHandlerTest()
        {
            _repositoryWrapper = A.Fake<IRepositoryWrapper>();
            _mapper = A.Fake<IMapper>();
            _handler = new AddDogHandler(_repositoryWrapper, _mapper);
        }

        [Fact]
        public async Task Handle_IfDogWithInsertedNameExist_ThrowInvalidOperationException()
        {
            var addDogRequest = new AddDogRequest("MyNewDog", "Black", 10, 10);

            A.CallTo(() => _repositoryWrapper.Dogs.AnyAsync(
                A<Expression<Func<DbDog, bool>>>._,
                A<CancellationToken>._))
                .Returns(true);

            Func<Task> actualResult = async () => await _handler.Handle(addDogRequest, CancellationToken.None);

            await actualResult.Should().ThrowAsync<InvalidOperationException>();

        }

        [Fact]
        public async Task Handle_IfDogWithInsertedNameExist_ReturnSuccess()
        {
            var addDogRequest = new AddDogRequest("MyNewDog", "Black", 10, 10);
            var insertedId = Guid.NewGuid();

            A.CallTo(() => _repositoryWrapper.Dogs.AnyAsync(
                A<Expression<Func<DbDog, bool>>>._,
                A<CancellationToken>._)).Returns(false);

            A.CallTo(() => _repositoryWrapper.Dogs.AddAsync(A<DbDog>._, A<CancellationToken>._))
                .Returns(insertedId);

            var actualResult = await _handler.Handle(addDogRequest, CancellationToken.None);

            actualResult.Should().Be(insertedId);
        }
    }
}