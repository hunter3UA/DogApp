using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Requests.Dog
{
    public sealed record AddDogRequest(
        string Name,
        string Color,
        double TailLength,
        double Weight) : IRequest<Result<Guid>>;
}