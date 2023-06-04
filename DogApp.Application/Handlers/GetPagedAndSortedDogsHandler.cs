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
    public class GetPagedAndSortedDogsHandler : IRequestHandler<GetPagedAndSortedDataCommand<DogEntity>, Result<IEnumerable<DogEntity>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetPagedAndSortedDogsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DogEntity>>> Handle(GetPagedAndSortedDataCommand<DogEntity> request, CancellationToken cancellationToken)
        {
            var countOfDogs = await _repositoryWrapper.Dogs.CountAsync(cancellationToken);

            var sortingOrder = SortingHelper.ConvertToEnum(request.SortingOrder);
            var sortedExpression = ExpressionHelper.CreateSortedExpression<DbDog>(request.SortingAttribute!);
            var currentPage =  PaginationHelper.FormatCurrentPage(request.PageNumber, request.PageSize, countOfDogs);

            var pagedElements = await _repositoryWrapper.Dogs
                .GetRangeAsync(
                cancellationToken,
                sortedExpression,
                sortingOrder,
                (currentPage - 1) * request.PageSize, 
                request.PageSize);

            return _mapper.Map<List<DogEntity>>(pagedElements.ToList());
        }
    }
}
