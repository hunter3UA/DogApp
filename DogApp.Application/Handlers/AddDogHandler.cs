using AutoMapper;
using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.DbEntities;
using MediatR;

namespace DogApp.Application.Handlers
{
    public class AddDogHandler : IRequestHandler<AddDogRequest, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public AddDogHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddDogRequest request, CancellationToken cancellationToken)
        {
            bool isDogExist = await _repositoryWrapper.Dogs.AnyAsync(d => d.Name == request.Name, cancellationToken);

            if (isDogExist)
                throw new InvalidOperationException("Dog with this name - already exist");

            var dogToAdd = _mapper.Map<DbDog>(request);
            var addedId = await _repositoryWrapper.Dogs.AddAsync(dogToAdd, cancellationToken);
            await _repositoryWrapper.SaveChangesAsync(cancellationToken);

            return addedId;
        }
    }
}
