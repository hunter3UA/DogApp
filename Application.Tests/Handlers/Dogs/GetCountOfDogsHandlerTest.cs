using DogApp.Application.Handlers;
using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using FakeItEasy;
using Xunit;

namespace DogApp.Tests.Handlers.Dogs
{
    public class GetCountOfDogsHandlerTest
    {
        private readonly GetCountOfDogsHandler _handler;
        private readonly IRepositoryWrapper _repo;

        public GetCountOfDogsHandlerTest()
        {
            _repo = A.Fake<IRepositoryWrapper>();
            _handler = new GetCountOfDogsHandler(_repo);
        }

        [Fact]
        public async Task Handle_IfReuestIsCorrect_RerturnSuccess()
        {
            var result = await _handler.Handle(new GetCountOfDogsRequest(), CancellationToken.None);

            A.CallTo(() => _repo.Dogs.CountAsync(A<CancellationToken>._)).MustHaveHappened();
        }
    }
}
