using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using MediatR;

namespace DogApp.Application.Handlers
{
    public sealed class GetCountOfDogsHandler : IRequestHandler<GetCountOfDogsRequest, int>
    {
        private readonly IRepositoryWrapper _respositoryWrapper;

        public GetCountOfDogsHandler(IRepositoryWrapper respositoryWrapper)
        {
            _respositoryWrapper = respositoryWrapper;
        }

        public async Task<int> Handle(GetCountOfDogsRequest request, CancellationToken cancellationToken)
        {
            var countOfDogs = await _respositoryWrapper.Dogs.CountAsync(cancellationToken);

            return countOfDogs;
        }
    }
}
