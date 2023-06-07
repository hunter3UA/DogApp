using DogApp.Application.Dtos.Dog;
using DogApp.Domain.Constants;
using FluentValidation;

namespace DogApp.Application.Validators
{
    public class AddDogValidator : AbstractValidator<AddDogDto>
    {
        public AddDogValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Field can not be empty")
                .MaximumLength(EntityConstants.DogConstants.MaxDogNameLength)
                .WithMessage($"Filed should be less than {EntityConstants.DogConstants.MaxDogNameLength}")
                .Matches("^[a-zA-Z ]+$")
                .WithMessage($"Field should contains only alphabetic characters");

            RuleFor(p => p.Color)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage($"Field can not be empty")
                .MaximumLength(EntityConstants.DogConstants.MaxDogColorLength)
                .WithMessage($"Field should not be less than {EntityConstants.DogConstants.MaxDogColorLength} characters")
                .Matches("^[a-zA-Z& ]+$")
                .WithMessage($"Field should contains only alphabetic characters and spliting by '&'");

            RuleFor(p => p.TailLength)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"Field should be greater than or equal to 0")
                .LessThanOrEqualTo(EntityConstants.DogConstants.MaxDogTailLength)
                .WithMessage($"Field should be less than or equql to {EntityConstants.DogConstants.MaxDogTailLength}");

            RuleFor(p => p.Weight)
                .GreaterThan(0)
                .WithMessage($"Field should be greater than 0")
                .LessThan(EntityConstants.DogConstants.MaxDogWeight)
                .WithMessage($"Field should be less than {EntityConstants.DogConstants.MaxDogWeight}");
        }
    }
}