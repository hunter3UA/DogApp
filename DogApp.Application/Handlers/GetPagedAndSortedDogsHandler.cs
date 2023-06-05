using AutoMapper;
using DogApp.Application.Commands.Common;
using DogApp.Application.Helpers;
using DogApp.Application.Repositories;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Handlers
{
    public class GetPagedAndSortedDogsHandler : IRequestHandler<GetPagedAndSortedDataCommand<DogEntity>, Result<List<DogEntity>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetPagedAndSortedDogsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<List<DogEntity>>> Handle(GetPagedAndSortedDataCommand<DogEntity> request, CancellationToken cancellationToken)
        {
            var countOfDogs = await _repositoryWrapper.Dogs.CountAsync(cancellationToken);

            var sortingOrder = SortingHelper.ConvertToEnum(request.SortingOrder);
            var sortedExpression = ExpressionHelper.CreateSortedExpression<DbDog>(request.SortingAttribute!);
            var pageSettings =  PaginationHelper.FormatCurrentPage(request.PageNumber, request.PageSize, countOfDogs);

            var pagedElements = await _repositoryWrapper.Dogs
                .GetRangeAsync(
                cancellationToken,
                sortedExpression,
                sortingOrder,
                pageSettings.PageNumber, 
                pageSettings.PageSize);
            
            return _mapper.Map<List<DogEntity>>(pagedElements);
        }
    }
}
