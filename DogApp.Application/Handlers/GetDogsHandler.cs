using AutoMapper;
using DogApp.Application.Helpers;
using DogApp.Application.Repositories;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Entities;
using MediatR;

namespace DogApp.Application.Handlers
{
    public class GetDogsHandler : IRequestHandler<GetDogsRequest,List<DogEntity>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetDogsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<List<DogEntity>> Handle(GetDogsRequest request, CancellationToken cancellationToken)
        {
            var sortingOrder = SortingHelper.ConvertToEnum(request.SortingOrder);
            var sortedExpression = ExpressionHelper.CreateSortedExpression<DbDog>(request.SortingAttribute);

            var pagedElements = await _repositoryWrapper.Dogs
                .GetRangeAsync(
                cancellationToken,
                sortedExpression,
                sortingOrder,
                request.Skip * request.Take,
                request.Take);

            return _mapper.Map<List<DogEntity>>(pagedElements);
        }
    }
}