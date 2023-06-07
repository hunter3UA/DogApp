using DogApp.Domain.Entities;
using MediatR;

namespace DogApp.Application.Requests.Dog
{
    public sealed record GetDogsRequest(
        int Skip,
        int Take,
        string? SortingOrder,
        string? SortingAttribute) : IRequest<List<DogEntity>>;
}