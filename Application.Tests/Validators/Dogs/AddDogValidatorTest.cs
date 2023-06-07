using DogApp.Application.Dtos.Dog;
using DogApp.Application.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using FluentValidation.Validators.UnitTestExtension.Core;
using Xunit;

namespace Application.Tests.Validators.Dogs
{
    public class AddDogValidatorTest
    {
        private readonly AddDogValidator _validator;

        public AddDogValidatorTest()
        {
            _validator = new AddDogValidator();
        }

        [Fact]
        public async Task Validate_IfValidDtoProvided_ShouldPassValidation()
        {
            var dto = new AddDogDto("Nomran", "Black", 10, 1);

            var result = await _validator.TestValidateAsync(dto, null, CancellationToken.None);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_IfEmptyNameOrColorProvided_ShouldFailValidation()
        {
            var dto = new AddDogDto("", "", 10, 5);

            var actualResult = await _validator.TestValidateAsync(dto, null, CancellationToken.None);

            actualResult.Errors.Count().Should().Be(2);
            actualResult
                .ShouldHaveValidationErrorFor(nameof(AddDogDto.Name))
                .WithErrorMessage("Field can not be empty");
            actualResult
                .ShouldHaveValidationErrorFor(nameof(AddDogDto.Color))
                .WithErrorMessage("Field can not be empty");
        }

        [Fact]
        public async Task Validate_IfInCorrectNameOrColorProvided_ShouldFailValidaton()
        {
            var dto = new AddDogDto("_)+", "Col_()+", 10, 5);

            var actualResult = await _validator.TestValidateAsync(dto, null, CancellationToken.None);

            actualResult.Errors.Count().Should().Be(2);
            actualResult
                .ShouldHaveValidationErrorFor(nameof(AddDogDto.Name))
                .WithErrorMessage("Field should contains only alphabetic characters");
            actualResult
               .ShouldHaveValidationErrorFor(nameof(AddDogDto.Color))
               .WithErrorMessage("Field should contains only alphabetic characters and spliting by '&'");
        }

        [Fact]
        public async Task Validate_IfInCorrectTailLengthOrWeightProvided_ShouldFailValidaton()
        {
            var dto = new AddDogDto("Nomran", "Black", -1, -1);

            var actualResult = await _validator.TestValidateAsync(dto, null, CancellationToken.None);

            actualResult.Errors.Count().Should().Be(2);
            actualResult
                .ShouldHaveValidationErrorFor(nameof(AddDogDto.TailLength));
            actualResult
               .ShouldHaveValidationErrorFor(nameof(AddDogDto.Weight));
        }
    }
}