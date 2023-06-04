using DogApp.Application.Dtos.Dog;
using DogApp.Domain.Constants;
using FluentValidation;

namespace DogApp.Application.Validators
{
    public class AddDogValidator : AbstractValidator<AddDogDto>
    {
        private const string NotEmpty = "can not be empty";

        public AddDogValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .WithMessage($"Field '{nameof(AddDogDto.Name)}' {NotEmpty}")
                .NotEmpty()
                .WithMessage($"Field '{nameof(AddDogDto.Name)}' {NotEmpty}")
                .MaximumLength(EntityConstants.DogConstants.MaxDogNameLength)
                .WithMessage($"Filed '{nameof(AddDogDto.Name)} should be less than {EntityConstants.DogConstants.MaxDogNameLength}")
                .Matches("^[a-zA-Z ]+$")
                .WithMessage($"Field '{nameof(AddDogDto.Name)}' should contains only alphabetic characters");

            RuleFor(p => p.Color)
                .NotNull()
                .WithMessage($"Field '{nameof(AddDogDto.Color)}' {NotEmpty}")
                .NotEmpty()
                .WithMessage($"Field '{nameof(AddDogDto.Color)}' {NotEmpty}")
                .MaximumLength(EntityConstants.DogConstants.MaxDogColorLength)
                .WithMessage($"Field '{nameof(AddDogDto.Color)} should not be less than {EntityConstants.DogConstants.MaxDogColorLength} characters")
                .Matches("^[a-zA-Z& ]+$")
                .WithMessage($"Field '{nameof(AddDogDto.Color)} should contains only alphabetic characters and spliting by &'");
                
            RuleFor(p => p.TailLength)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"Field '{nameof(AddDogDto.TailLength)}' should be greater than or equal to 0")
                .LessThanOrEqualTo(EntityConstants.DogConstants.MaxDogTailLength)
                .WithMessage($"Field '{nameof(AddDogDto.TailLength)}' should be less than or equql to {EntityConstants.DogConstants.MaxDogTailLength}");

            RuleFor(p => p.Weight)
                .GreaterThan(0)
                .WithMessage($"Field '{nameof(AddDogDto.Weight)}' should be greater than 0")
                .LessThan(EntityConstants.DogConstants.MaxDogWeight)
                .WithMessage($"Field '{nameof(AddDogDto.Weight)} should be less than {EntityConstants.DogConstants.MaxDogWeight}");
        }
    }
}
