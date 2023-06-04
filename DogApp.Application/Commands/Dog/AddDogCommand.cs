using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Commands.Dog
{
    public sealed record AddDogCommand(
        string Name,
        string Color,
        double TailLength,
        double Weight) : IRequest<Result<Guid>>;
}