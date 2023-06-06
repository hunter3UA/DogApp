using DogApp.Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Requests.Dog
{
    public sealed record GetSortedDogsRequest(
        int Skip,
        int Take,
        string? SortingOrder,
        string? SortingAttribute) : IRequest<List<DogEntity>>;
}
