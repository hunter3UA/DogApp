using MediatR;

namespace DogApp.Application.Requests.Dog
{
    public sealed record GetCountOfDogsRequest : IRequest<int>;
}
